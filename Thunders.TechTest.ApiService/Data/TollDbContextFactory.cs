using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace Thunders.TechTest.ApiService.Data;

public class TollDbContextFactory : IDesignTimeDbContextFactory<TollDbContext>
{
    public TollDbContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var builder = new DbContextOptionsBuilder<TollDbContext>();
        var connectionString = "Server=127.0.0.1,58318;User ID=sa;Password=Mypassword1570!;TrustServerCertificate=true;Database=ThundersTechTest";
            //configuration.GetConnectionString("ThundersTechTestDb");

        builder.UseSqlServer(
            connectionString,
            b => b.MigrationsAssembly("Thunders.TechTest.ApiService")
        );

        return new TollDbContext(builder.Options);
    }
}