using Microsoft.EntityFrameworkCore;
using Thunders.TechTest.ApiService.Application.Reports.GetInfoReport;
using Thunders.TechTest.ApiService.CrossCutting.Mediator.Abstractions;
using Thunders.TechTest.ApiService.CrossCutting.Validations;
using Thunders.TechTest.ApiService.Data;
using Thunders.TechTest.ApiService.Domain.Reports;

namespace Thunders.TechTest.ApiService.Application.Reports.ScheduleReport;

public class GetReportInfoHandler(
    TollDbContext dbContext,
    ValidationContext validationContext)
    : ICommandHandler<GetReportInfoCommand, GetReportInfoResult>
{
    public async Task<GetReportInfoResult> HandleAsync(
        GetReportInfoCommand command,
        CancellationToken cancellationToken)
    {
        var report = await dbContext.Set<Report>()
            .Where(r => r.Id == command.ReportId)
            .Select(r => new
            {
                r.Id,
                r.FileName,
                r.Status,
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (report is null)
        {
            validationContext.AddNotFound(
                nameof(command.ReportId),
                nameof(Report),
                command.ReportId);
            return new();
        }

        return new()
        {
            Id = report.Id,
            FileName = report.FileName,
            Status = report.Status,
        };
    }
}