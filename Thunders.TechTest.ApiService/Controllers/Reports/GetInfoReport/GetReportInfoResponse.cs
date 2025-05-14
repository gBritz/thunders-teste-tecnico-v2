using Thunders.TechTest.ApiService.Application.Reports.GetInfoReport;
using Thunders.TechTest.ApiService.Domain.Reports;

namespace Thunders.TechTest.ApiService.Controllers.Reports.GetInfoReport;

public record GetReportInfoResponse
{
    public Guid Id { get; init; }
    public string FileName { get; init; }
    public ReportStatusType Status { get; init; }

    public GetReportInfoResponse(GetReportInfoResult result)
    {
        ArgumentNullException.ThrowIfNull(result, nameof(result));

        Id = result.Id;
        FileName = result.FileName!;
        Status = result.Status;
    }
}