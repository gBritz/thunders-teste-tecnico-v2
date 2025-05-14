using Thunders.TechTest.ApiService.Domain.Reports;

namespace Thunders.TechTest.ApiService.Application.Reports.GetInfoReport;

public record GetReportInfoResult
{
    public Guid Id { get; init; }
    public string? FileName { get; init; }
    public ReportStatusType Status { get; init; }
}