using Thunders.TechTest.ApiService.Domain.Reports.Execution;
using Thunders.TechTest.ApiService.Domain.Reports;

namespace Thunders.TechTest.ApiService.Services.Reports;

public class FileGeneratorExecutor<T>(
    IReportDataCollector<T> dataCollector,
    IReportFileGenerator<T> fileGenerator) :
    IReportExecutor
{
    public object? Data { get; private set; }

    public async Task<Stream> ExecuteReportGenerationAsync(Report report, CancellationToken cancellationToken)
    {
        var data = await dataCollector.CollectDataAsync(report, cancellationToken);
        Data = data;
        return fileGenerator.GenerateFile(data);
    }
}