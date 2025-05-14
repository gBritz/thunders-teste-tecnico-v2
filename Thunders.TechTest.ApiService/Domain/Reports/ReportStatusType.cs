namespace Thunders.TechTest.ApiService.Domain.Reports;

/// <summary>
/// Defines a status of report.
/// </summary>
public enum ReportStatusType
{
    /// <summary>
    /// Report process not started.
    /// </summary>
    New = 1,

    /// <summary>
    /// Report process was stated.
    /// </summary>
    Started = 2,

    /// <summary>
    /// Report generated successfully.
    /// </summary>
    Generated = 3,

    /// <summary>
    /// Error happens during report process.
    /// </summary>
    Error = 4,
}