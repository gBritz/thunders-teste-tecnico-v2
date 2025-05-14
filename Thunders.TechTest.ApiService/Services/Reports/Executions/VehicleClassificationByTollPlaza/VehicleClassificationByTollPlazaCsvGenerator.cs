using CsvHelper;
using System.Globalization;
using Thunders.TechTest.ApiService.Domain.Reports.Execution;

namespace Thunders.TechTest.ApiService.Services.Reports.Executions.VehicleClassificationByTollPlaza;

public class VehicleClassificationByTollPlazaCsvGenerator :
    IReportFileGenerator<VehicleClassificationByTollPlazaData>
{
    public Stream GenerateFile(VehicleClassificationByTollPlazaData data)
    {
        var portugueseCulture = new CultureInfo("pt-BR");
        var buffer = new MemoryStream();

        var lines = data.Plazas.SelectMany(p => p.Vehicles.Select(v => new
        {
            p.TollPlazaName,
            v.VehicleName,
            v.Quantity,
        }));

        using (var writer = new StreamWriter(buffer))
        using (var csv = new CsvWriter(writer, portugueseCulture))
        {
            csv.WriteRecords(lines);
            writer.Flush();
        }

        return buffer;
    }
}