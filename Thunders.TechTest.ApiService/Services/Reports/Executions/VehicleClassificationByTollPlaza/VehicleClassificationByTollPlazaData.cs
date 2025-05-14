using Thunders.TechTest.ApiService.Domain;

namespace Thunders.TechTest.ApiService.Services.Reports.Executions.VehicleClassificationByTollPlaza;

public record VehicleClassificationByTollPlazaData
{
    public required ICollection<PlazaVehiclesData> Plazas { get; init; }
}

public record PlazaVehiclesData
{
    public required string TollPlazaName { get; init; }
    public required ICollection<VehicleData> Vehicles { get; init; }
}

public record VehicleData
{
    public VehicleType Vehicle { get; init; }
    public long Quantity { get; init; }

    public string VehicleName =>
        Vehicle switch
        {
            VehicleType.Car => "Carro",
            VehicleType.Motorcycle => "Moto",
            VehicleType.Truck => "Caminhão",

            _ => throw new NotImplementedException(),
        };
}