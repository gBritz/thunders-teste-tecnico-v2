using Thunders.TechTest.ApiService.Domain.Reports.Execution;

namespace Thunders.TechTest.ApiService.Services.Reports.Executions.TopEarningTollPlazasPerMonth;

public class TopEarningTollPlazasPerMonthPdfGenerator :
    IReportFileGenerator<TopEarningTollPlazasPerMonthData>
{
    public Stream GenerateFile(TopEarningTollPlazasPerMonthData data)
    {
        return new MemoryStream();
    }
}