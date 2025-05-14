using Thunders.TechTest.ApiService.Messaging.Reports.GenerateReport;

namespace Thunders.TechTest.ApiService.Services.Reports.Executions.TotalPerHourByCity;

public record TotalPerHourByCityData
{
    public required ICollection<CityData> Cities { get; init; }
}

public record CityData
{
    public required string CityName { get; init; }

    public required ICollection<HourData> Hours { get; init; }
}

public record HourData
{
    public required int Hour { get; init; }

    public required decimal Total { get; init; }
}