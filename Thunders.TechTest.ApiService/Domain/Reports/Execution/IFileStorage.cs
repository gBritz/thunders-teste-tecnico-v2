namespace Thunders.TechTest.ApiService.Domain.Reports.Execution;

public interface IFileStorage
{
    Task StoreAsync(string fileName, string folder, Stream file, Report report);
}