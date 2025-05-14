using Thunders.TechTest.ApiService.Domain.Reports.Execution;

namespace Thunders.TechTest.ApiService.Services.Reports.Executions.VehicleClassificationByTollPlaza;

public class VehicleClassificationByTollPlazaPdfGenerator :
    IReportFileGenerator<VehicleClassificationByTollPlazaData>
{
    public Stream GenerateFile(VehicleClassificationByTollPlazaData data)
    {
        return new MemoryStream();
    }
}