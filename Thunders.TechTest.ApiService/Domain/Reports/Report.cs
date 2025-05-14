using Thunders.TechTest.ApiService.Abstractions;

namespace Thunders.TechTest.ApiService.Domain.Reports;

/// <summary>
/// Generic scheduled report.
/// </summary>
public class Report : IEntity
{
    public Guid Id { get; init; }

    public DateTimeOffset CreatedAt { get; init; }

    public DateTimeOffset? FinishedAt { get; set; }

    public TimeSpan? ElapsedExecution { get; set; }

    public required string FileName { get; init; }

    public required ReportStatusType Status { get; set; } = ReportStatusType.New;

    public required ReportType Type { get; init; }

    public required ReportFormatType FormatType { get; init; }

    public string? ErrorMessage { get; set; }

    public byte[]? BlobFile { get; set; }

    public string? JsonResultData { get; set; }

	public string? PostbackUrl { get; set; }

    public ICollection<ReportParameter> Parameters { get; init; } = [];

    public ReportParameter? GetParameterByName(string parameterName) =>
        Parameters.FirstOrDefault(p => p.Name.Equals(parameterName, StringComparison.OrdinalIgnoreCase));
}