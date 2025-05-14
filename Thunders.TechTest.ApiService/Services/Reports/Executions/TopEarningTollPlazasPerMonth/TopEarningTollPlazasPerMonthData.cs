using System.Globalization;

namespace Thunders.TechTest.ApiService.Services.Reports.Executions.TopEarningTollPlazasPerMonth;

public record TopEarningTollPlazasPerMonthData
{
    public required ICollection<TopEarningMonthly> Months { get; init; }
}

public record TopEarningMonthly
{
    public int MonthNumber { get; init; }
    public required ICollection<TopEarningPlaza> Plazas { get; init; }

    public string GetMonthName(CultureInfo culture)
    {
        ArgumentNullException.ThrowIfNull(culture, nameof(culture));

        return culture.DateTimeFormat.GetMonthName(MonthNumber);
    }
}

public record TopEarningPlaza
{
    public required string TollPlazaName { get; init; }
    public decimal Total { get; init; }
}