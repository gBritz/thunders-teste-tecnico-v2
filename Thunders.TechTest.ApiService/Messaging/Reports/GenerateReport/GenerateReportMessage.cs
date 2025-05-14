namespace Thunders.TechTest.ApiService.Messaging.Reports.GenerateReport;

public record GenerateReportMessage
{
    public required Guid ReportId { get; init; }
}