using Thunders.TechTest.ApiService.Data;
using Thunders.TechTest.ApiService.Domain.Reports.Execution;
using Thunders.TechTest.ApiService.Domain.Reports;
using Thunders.TechTest.ApiService.Domain.Tolls;
using Microsoft.EntityFrameworkCore;

namespace Thunders.TechTest.ApiService.Services.Reports.Executions.TotalPerHourByCity;

public class TotalPerHourByCityDataCollector(TollDbContext dbContext) :
    IReportDataCollector<TotalPerHourByCityData>
{
    public async Task<TotalPerHourByCityData> CollectDataAsync(
        Report report,
        CancellationToken cancellationToken)
    {
        var cities = await dbContext.Set<TollPlaza>()
            .GroupBy(p => p.Id)
            .Select(g => new CityData
            {
                CityName = g.First().City,
                Hours = g.SelectMany(t => t.Payments)
                    .GroupBy(p => p.PaidAt.Hour)
                    .Select(pg => new HourData
                    {
                        Hour = pg.Key,
                        Total = pg.Sum(_ => _.Amount)
                    })
                    .OrderBy(r => r.Hour)
                    .ToArray(),
            })
            .ToArrayAsync(cancellationToken);

        return new()
        {
            Cities = cities,
        };
    }
}