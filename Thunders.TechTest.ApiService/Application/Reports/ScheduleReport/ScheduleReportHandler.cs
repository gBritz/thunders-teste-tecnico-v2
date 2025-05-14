using Thunders.TechTest.ApiService.CrossCutting.Mediator.Abstractions;
using Thunders.TechTest.ApiService.Data;
using Thunders.TechTest.ApiService.Domain.Reports;
using Thunders.TechTest.ApiService.Messaging.Reports.GenerateReport;
using Thunders.TechTest.OutOfBox.Queues;

namespace Thunders.TechTest.ApiService.Application.Reports.ScheduleReport;

public class ScheduleReportHandler(
    TollDbContext dbContext,
    IMessageSender messageSender)
    : ICommandHandler<ScheduleReportCommand, ScheduleReportResult>
{
    public async Task<ScheduleReportResult> HandleAsync(
        ScheduleReportCommand command,
        CancellationToken cancellationToken)
    {
        var report = CreateDomainFrom(command);
        dbContext.Add(report);

        await dbContext.SaveChangesAsync(cancellationToken);

        await messageSender.SendLocal(new GenerateReportMessage
        {
            ReportId = report.Id,
        });

        return new()
        {
            ReportId = report.Id,
        };
    }

    private static Report CreateDomainFrom(ScheduleReportCommand command)
    {
        return new()
        {
            Type = command.Type,
            FileName = command.FileName,
            FormatType = command.FormatType,
            PostbackUrl = command.PostbackUrl,
            Status = ReportStatusType.New,
            CreatedAt = DateTime.UtcNow,
            Parameters = command.Parameters
                .Select(p => new ReportParameter
                {
                    Name = p.Name,
                    Value = p.Value,
                })
                .ToArray(),
        };
    }
}