using Thunders.TechTest.ApiService.Data;
using Thunders.TechTest.ApiService.Domain.Reports.Execution;
using Thunders.TechTest.ApiService.Domain.Reports;
using Thunders.TechTest.ApiService.Domain.Tolls;
using Microsoft.EntityFrameworkCore;

namespace Thunders.TechTest.ApiService.Services.Reports.Executions.VehicleClassificationByTollPlaza;

public class VehicleClassificationByTollPlazaDataCollector(TollDbContext dbContext) :
    IReportDataCollector<VehicleClassificationByTollPlazaData>
{
    public async Task<VehicleClassificationByTollPlazaData> CollectDataAsync(
        Report report,
        CancellationToken cancellationToken)
    {
        Guid[]? plazasIdsToFilter = null;
        var maxPlazasPerMonthParameter = report.GetParameterByName("PLAZAS_IDS");
        if (maxPlazasPerMonthParameter is not null)
        {
            plazasIdsToFilter = maxPlazasPerMonthParameter.Value.Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(i => Guid.TryParse(i, out var result) ? result : Guid.Empty)
                .Where(i => i != Guid.Empty)
                .ToArray();
        }

        IQueryable<TollPayment> query = dbContext.Set<TollPayment>();

        if (plazasIdsToFilter?.Length > 0)
        {
            query = query.Where(q => plazasIdsToFilter.Contains(q.Plaza.Id));
        }

        var plazas = await
            query.GroupBy(p => p.Plaza.Id)
            .Select(g => new PlazaVehiclesData
            {
                TollPlazaName = $"{g.First().Plaza.Concessionaire.CompanyName} {g.First().Plaza.City}",
                Vehicles = g.GroupBy(_ => _.Vehicle)
                    .Select(_ => new VehicleData
                    {
                        Vehicle = _.Key,
                        Quantity = _.LongCount()
                    })
                    .ToArray(),
            })
            .ToArrayAsync(cancellationToken);

        return new()
        {
            Plazas = plazas,
        };
    }
}