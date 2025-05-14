using Thunders.TechTest.ApiService.CrossCutting.Mediator.Abstractions;

namespace Thunders.TechTest.ApiService.Application.Reports.GetInfoReport;

public record GetReportInfoCommand : ICommand<GetReportInfoResult>
{
    public required Guid ReportId { get; init; }
}