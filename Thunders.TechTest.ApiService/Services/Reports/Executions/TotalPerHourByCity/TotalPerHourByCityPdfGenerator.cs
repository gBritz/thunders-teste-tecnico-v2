using Thunders.TechTest.ApiService.Domain.Reports.Execution;

namespace Thunders.TechTest.ApiService.Services.Reports.Executions.TotalPerHourByCity;

public class TotalPerHourByCityPdfGenerator :
    IReportFileGenerator<TotalPerHourByCityData>
{
    public Stream GenerateFile(TotalPerHourByCityData data)
    {
        return new MemoryStream();
    }
}