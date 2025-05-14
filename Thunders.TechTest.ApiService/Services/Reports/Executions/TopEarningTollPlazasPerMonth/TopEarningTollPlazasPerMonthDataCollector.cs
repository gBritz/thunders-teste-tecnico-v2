using Microsoft.EntityFrameworkCore;
using Thunders.TechTest.ApiService.Data;
using Thunders.TechTest.ApiService.Domain.Reports;
using Thunders.TechTest.ApiService.Domain.Reports.Execution;
using Thunders.TechTest.ApiService.Domain.Tolls;

namespace Thunders.TechTest.ApiService.Services.Reports.Executions.TopEarningTollPlazasPerMonth;

public class TopEarningTollPlazasPerMonthDataCollector(TollDbContext dbContext) :
    IReportDataCollector<TopEarningTollPlazasPerMonthData>
{
    public int MaximumPlazasPerMonth { get; set; }

    public async Task<TopEarningTollPlazasPerMonthData> CollectDataAsync(
        Report report,
        CancellationToken cancellationToken)
    {
        var maxPlazasPerMonth = MaximumPlazasPerMonth;
        var maxPlazasPerMonthParameter = report.GetParameterByName("MAX_PLAZAS_PER_MONTH");
        if (maxPlazasPerMonthParameter is not null && maxPlazasPerMonthParameter.ValueAsInt() <= MaximumPlazasPerMonth)
        {
            maxPlazasPerMonth = maxPlazasPerMonthParameter.ValueAsInt();
        }

        var ratedMonthly = await dbContext.Set<TollPayment>()
            .GroupBy(p => p.PaidAt.Month)
            .Select(g => new TopEarningMonthly
            {
                MonthNumber = g.Key,
                Plazas = g.GroupBy(_ => _.Plaza.Id)
                    .Select(g => new TopEarningPlaza
                    {
                        TollPlazaName = $"{g.First().Plaza.Concessionaire.CompanyName} {g.First().Plaza.City}",
                        Total = g.Sum(s => s.Amount),
                    })
                    .OrderByDescending(o => o.Total)
                    .Take(maxPlazasPerMonth)
                    .ToArray(),
            })
            .ToArrayAsync(cancellationToken);

        return new()
        {
            Months = ratedMonthly,
        };
    }
}