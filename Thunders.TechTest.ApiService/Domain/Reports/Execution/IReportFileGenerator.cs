namespace Thunders.TechTest.ApiService.Domain.Reports.Execution;

public interface IReportFileGenerator<T>
{
    Stream GenerateFile(T data);
}