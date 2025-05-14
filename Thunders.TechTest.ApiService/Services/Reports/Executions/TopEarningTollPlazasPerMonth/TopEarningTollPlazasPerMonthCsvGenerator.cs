using CsvHelper;
using System.Globalization;
using Thunders.TechTest.ApiService.Domain.Reports.Execution;

namespace Thunders.TechTest.ApiService.Services.Reports.Executions.TopEarningTollPlazasPerMonth;

public class TopEarningTollPlazasPerMonthCsvGenerator :
    IReportFileGenerator<TopEarningTollPlazasPerMonthData>
{
    public Stream GenerateFile(TopEarningTollPlazasPerMonthData data)
    {
        var portugueseCulture = new CultureInfo("pt-BR");
        var buffer = new MemoryStream();

        var lines = data.Months.SelectMany(m => m.Plazas.Select(p => new
        {
            Month = m.GetMonthName(portugueseCulture),
            p.TollPlazaName,
            p.Total,
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