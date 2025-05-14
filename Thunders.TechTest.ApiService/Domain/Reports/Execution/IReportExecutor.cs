namespace Thunders.TechTest.ApiService.Domain.Reports.Execution;

public interface IReportExecutor
{
    object? Data { get; }
    Task<Stream> ExecuteReportGenerationAsync(Report report, CancellationToken cancellationToken);
}