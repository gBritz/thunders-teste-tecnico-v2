using CsvHelper;
using System.Globalization;
using Thunders.TechTest.ApiService.Domain.Reports.Execution;

namespace Thunders.TechTest.ApiService.Services.Reports.Executions.TotalPerHourByCity;

public class TotalPerHourByCityCsvGenerator :
    IReportFileGenerator<TotalPerHourByCityData>
{
    public Stream GenerateFile(TotalPerHourByCityData data)
    {
        var buffer = new MemoryStream();

        var lines = data.Cities.SelectMany(c => c.Hours.Select(h => new
        {
            c.CityName,
            h.Hour,
            h.Total,
        }));

        using (var writer = new StreamWriter(buffer))
        using (var csv = new CsvWriter(writer, new CultureInfo("pt-BR")))
        {
            csv.WriteRecords(lines);
            writer.Flush();
        }

        return buffer;
    }
}