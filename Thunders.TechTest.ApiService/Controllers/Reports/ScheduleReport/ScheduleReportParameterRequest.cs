namespace Thunders.TechTest.ApiService.Controllers.Reports.ScheduleReport;

/// <summary>
/// Represents a request that report parameter.
/// </summary>
public record ScheduleReportParameterRequest
{
    /// <summary>
    /// Parameter name.
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// Parameter value.
    /// </summary>
    public required string Value { get; init; }
}