using Thunders.TechTest.ApiService.Abstractions;

namespace Thunders.TechTest.ApiService.Domain.Reports;

/// <summary>
/// Parameters of report.
/// </summary>
public class ReportParameter : IEntity
{
    public Guid Id { get; init; }

    public required string Name { get; init; }

    public required string Value { get; init; }

    public int ValueAsInt() => Convert.ToInt32(Value);

    public long ValueAsLong() => Convert.ToInt64(Value);

    public Guid[] ValueAsArrayOfGuid() =>
        Value.Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select(i => Guid.TryParse(i, out var result) ? result : Guid.Empty)
            .Where(i => i != Guid.Empty)
            .ToArray();
}