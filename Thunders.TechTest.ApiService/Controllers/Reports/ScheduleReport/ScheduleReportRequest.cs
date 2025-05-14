using Thunders.TechTest.ApiService.Application.Reports.ScheduleReport;
using Thunders.TechTest.ApiService.Domain.Reports;

namespace Thunders.TechTest.ApiService.Controllers.Reports.ScheduleReport;

/// <summary>
/// Represents a request that schedule report generation.
/// </summary>
public record ScheduleReportRequest
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
    /// Parameters.
    /// </summary>
    /// <remarks>Optional</remarks>
    public ICollection<ScheduleReportParameterRequest> Parameters { get; init; } = [];

    public ScheduleReportCommand AsCommand()
    {
        return new()
        {
            FileName = FileName,
            FormatType = FormatType,
            Type = Type,
            PostbackUrl = PostbackUrl,
            Parameters = Parameters.Select(p => new ScheduleReportParameterCommand
            {
                Name = p.Name,
                Value = p.Value,
            }).ToArray(),
        };
    }
}