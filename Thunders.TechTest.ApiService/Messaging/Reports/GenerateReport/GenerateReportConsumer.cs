using System.Diagnostics;
using Rebus.Handlers;
using Thunders.TechTest.ApiService.Data;
using Thunders.TechTest.ApiService.Domain.Reports;

namespace Thunders.TechTest.ApiService.Messaging.Reports.GenerateReport;

public class GenerateReportConsumer(
    TollDbContext tollDbContext,
    ILogger<GenerateReportConsumer> logger)
    : IHandleMessages<GenerateReportMessage>
{
    public async Task Handle(GenerateReportMessage message)
    {
        var timer = Stopwatch.StartNew();
        var report = tollDbContext.Find<Report>(message.ReportId);

        if (report is null)
        {
            logger.LogError($"Not found report #{message.ReportId} to generate.");
            return;
        }

        report.Status = ReportStatusType.Started;

        await tollDbContext.SaveChangesAsync();

        try
        {
            await GenerateReportAsync(report);
            report.Status = ReportStatusType.Generated;
        }
        catch (Exception ex)
        {
            report.Status = ReportStatusType.Error;
            report.ErrorMessage = ex.Message;
        }
        finally
        {
            timer.Stop();
            report.ElapsedExecution = timer.Elapsed;
            report.FinishedAt = DateTime.UtcNow;
            await tollDbContext.SaveChangesAsync();
        }
    }

    private Task GenerateReportAsync(Report report)
    {
        return Task.CompletedTask;
    }
}