using Thunders.TechTest.ApiService.Domain.Reports;
using Thunders.TechTest.ApiService.Domain.Reports.Execution;

namespace Thunders.TechTest.ApiService.Services.Reports;

public class LocalFileStorage : IFileStorage
{
    public Task StoreAsync(string fileName, string folder, Stream file, Report report)
    {
        var buffer = file is MemoryStream memo ? memo.GetBuffer() : null;
        return File.WriteAllBytesAsync($"C:\\Users\\guigu\\Desktop\\teste\\{report.FileName}", buffer);
    }
}