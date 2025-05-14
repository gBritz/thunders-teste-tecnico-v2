namespace Thunders.TechTest.ApiService.Domain.Reports.Execution;

public interface IReportExecutorFactory
{
    IReportExecutor CreateInstanceBy(ReportType type, ReportFormatType format);
}