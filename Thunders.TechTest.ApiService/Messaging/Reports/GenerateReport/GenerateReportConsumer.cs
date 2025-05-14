using System.Diagnostics;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Rebus.Handlers;
using Thunders.TechTest.ApiService.Data;
using Thunders.TechTest.ApiService.Domain.Reports;
using Thunders.TechTest.ApiService.Domain.Reports.Execution;

namespace Thunders.TechTest.ApiService.Messaging.Reports.GenerateReport;

public class GenerateReportConsumer(
    TollDbContext tollDbContext,
    IFileStorage fileStorage,
    IReportExecutorFactory executorFactory,
    ILogger<GenerateReportConsumer> logger)
    : IHandleMessages<GenerateReportMessage>
{
    public async Task Handle(GenerateReportMessage message)
    {
        var timer = Stopwatch.StartNew();
        var report = await tollDbContext
            .Set<Report>()
            .Include(_ => _.Parameters)
            .FirstOrDefaultAsync(r => r.Id == message.ReportId);

        if (report is null)
        {
            logger.LogError($"Not found report #{message.ReportId} to generate.");
            return;
        }

        logger.LogInformation($"Report #{report.Id} started for type {report.Type} in format {report.FormatType}");
        report.Status = ReportStatusType.Started;

        await tollDbContext.SaveChangesAsync();

        try
        {
            await GenerateReportAsync(report, default);
            report.Status = ReportStatusType.Generated;
            logger.LogInformation($"Report #{report.Id} generated successfully for type {report.Type} in format {report.FormatType}");
        }
        catch (Exception ex)
        {
            report.Status = ReportStatusType.Error;
            report.ErrorMessage = ex.Message;
            logger.LogError(ex, $"Report #{report.Id} had error for type {report.Type} in format {report.FormatType}");
        }
        finally
        {
            timer.Stop();
        }

        report.ElapsedExecution = timer.Elapsed;
        report.FinishedAt = DateTime.UtcNow;

        await tollDbContext.SaveChangesAsync();

        logger.LogInformation($"Report #{report.Id} completed in {timer.Elapsed} for type {report.Type} in format {report.FormatType}");
    }

    private async Task GenerateReportAsync(
        Report report,
        CancellationToken cancellationToken)
    {
        var reportExecutor = executorFactory.CreateInstanceBy(report.Type, report.FormatType);

        using var fileStream = await reportExecutor.ExecuteReportGenerationAsync(report, cancellationToken);

        report.JsonResultData = JsonSerializer.Serialize(reportExecutor.Data);
        report.BlobFile = fileStream is MemoryStream memo ? memo.GetBuffer() : null;

        await fileStorage.StoreAsync(
            report.FileName,
            string.Empty,
            fileStream,
            report);
    }
}