namespace Thunders.TechTest.ApiService.Application.Reports.ScheduleReport;

public record ScheduleReportParameterCommand
{
    public required string Name { get; init; }

    public required string Value { get; init; }
}