using Thunders.TechTest.ApiService.CrossCutting.Mediator.Abstractions;
using Thunders.TechTest.ApiService.Domain.Reports;

namespace Thunders.TechTest.ApiService.Application.Reports.ScheduleReport;

public record ScheduleReportCommand : ICommand<ScheduleReportResult>
{
    /// <summary>
    /// Name of file.
    /// </summary>
    public required string FileName { get; init; }

    /// <summary>
    /// Report type.
    /// </summary>
    public required ReportType Type { get; init; }

    /// <summary>
    /// Output format type.
    /// </summary>
    public required ReportFormatType FormatType { get; init; }

    /// <summary>
    /// Url to post back when finish report generation.
    /// </summary>
    public string? PostbackUrl { get; set; }

    /// <summary>
    /// Paramters to generate report.
    /// </summary>
    public ICollection<ScheduleReportParameterCommand> Parameters { get; init; } = [];
}