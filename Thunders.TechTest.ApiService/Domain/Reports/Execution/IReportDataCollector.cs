namespace Thunders.TechTest.ApiService.Domain.Reports.Execution;

public interface IReportDataCollector<T>
{
    Task<T> CollectDataAsync(Report report, CancellationToken cancellationToken);
}