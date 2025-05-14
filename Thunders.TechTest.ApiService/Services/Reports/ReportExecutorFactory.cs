using Thunders.TechTest.ApiService.Data;
using Thunders.TechTest.ApiService.Domain.Reports;
using Thunders.TechTest.ApiService.Domain.Reports.Execution;
using Thunders.TechTest.ApiService.Services.Reports.Executions.TopEarningTollPlazasPerMonth;
using Thunders.TechTest.ApiService.Services.Reports.Executions.TotalPerHourByCity;
using Thunders.TechTest.ApiService.Services.Reports.Executions.VehicleClassificationByTollPlaza;

namespace Thunders.TechTest.ApiService.Services.Reports;

public class ReportExecutorFactory(
    TollDbContext dbContext,
    IConfiguration configuration) :
    IReportExecutorFactory
{
    public IReportExecutor CreateInstanceBy(ReportType type, ReportFormatType format)
    {
        return type switch
        {
            ReportType.TotalPerHourByCity =>
                new FileGeneratorExecutor<TotalPerHourByCityData>(
                    new TotalPerHourByCityDataCollector(dbContext),
                    CreateGeneratorForTotalPerHourByCity(format)),

            ReportType.TopEarningTollPlazasPerMonth =>
                new FileGeneratorExecutor<TopEarningTollPlazasPerMonthData>(
                    new TopEarningTollPlazasPerMonthDataCollector(dbContext)
                    {
                        MaximumPlazasPerMonth = Convert.ToInt32(configuration["Report:MaxPlazasPerMonth"]),
                    },
                    CreateGeneratorForTopEarningTollPlazasPerMonth(format)),

            ReportType.VehicleClassificationByTollPlaza =>
                new FileGeneratorExecutor<VehicleClassificationByTollPlazaData>(
                    new VehicleClassificationByTollPlazaDataCollector(dbContext),
                    CreateGeneratorForVehicleClassificationByTollPlaza(format)),

            _ => throw new InvalidOperationException($"Not available file generator for type '{type}' in this moment."),
        };
    }

    private IReportFileGenerator<TotalPerHourByCityData> CreateGeneratorForTotalPerHourByCity(ReportFormatType format)
    {
        return format switch
        {
            ReportFormatType.Csv =>
                new TotalPerHourByCityCsvGenerator(),

            ReportFormatType.Pdf =>
                new TotalPerHourByCityPdfGenerator(),

            _ => throw new InvalidOperationException($"Not available file generator in format '{format}' for report TotalPerHourByCity.")
        };
    }

    private IReportFileGenerator<TopEarningTollPlazasPerMonthData> CreateGeneratorForTopEarningTollPlazasPerMonth(ReportFormatType format)
    {
        return format switch
        {
            ReportFormatType.Csv =>
                new TopEarningTollPlazasPerMonthCsvGenerator(),

            ReportFormatType.Pdf =>
                new TopEarningTollPlazasPerMonthPdfGenerator(),

            _ => throw new InvalidOperationException($"Not available file generator in format '{format}' for report TopEarningTollPlazasPerMonth.")
        };
    }

    private IReportFileGenerator<VehicleClassificationByTollPlazaData> CreateGeneratorForVehicleClassificationByTollPlaza(ReportFormatType format)
    {
        return format switch
        {
            ReportFormatType.Csv =>
                new VehicleClassificationByTollPlazaCsvGenerator(),

            ReportFormatType.Pdf =>
                new VehicleClassificationByTollPlazaPdfGenerator(),

            _ => throw new InvalidOperationException($"Not available file generator in format '{format}' for report VehicleClassificationByTollPlaza.")
        };
    }
}