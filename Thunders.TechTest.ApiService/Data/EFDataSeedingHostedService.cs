using Microsoft.EntityFrameworkCore;
using Thunders.TechTest.ApiService.Domain.Tolls;

namespace Thunders.TechTest.ApiService.Data;

internal class EFDataSeedingHostedService(IServiceProvider serviceProvider)
    : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = serviceProvider.CreateScope();
        await using var dbContext = scope.ServiceProvider.GetRequiredService<TollDbContext>();

        await dbContext.Database.EnsureCreatedAsync(cancellationToken);
        await dbContext.Database.MigrateAsync(cancellationToken);

        dbContext.AddRange(CreateTolls());

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken) =>
        Task.CompletedTask;

    private static ICollection<TollConcessionaire> CreateTolls()
    {
        // Fontes:
        // https://www.gov.br/antt/pt-br/assuntos/rodovias/concessionarias/lista-de-concessoes/ccr-viacosteira/localizacao-das-pracas-de-pedagio
        // https://app.powerbi.com/view?r=eyJrIjoiZGVhNGFiODItNWZiZi00MTg2LThjZWUtZjFlMjlkNTc5YWVlIiwidCI6Ijg3YmJlOWRlLWE4OTItNGNkZS1hNDY2LTg4Zjk4MmZiYzQ5MCJ9

        var ccrViaSul = new TollConcessionaire
        {
            CompanyName = "CCR VIA SUL",
            LegalDocument = "32161500000100",
            CreatedAt = DateTime.UtcNow,
            Plazas =
        {
            new()
            {
                City = "Gravataí",
                Highway = "BR-290",
                Kms = 60M,
                State = "RS",
                Longitude = -29.93M,
                Latitude = -50.86M,
                Concessionaire = default!,
            },
            new()
            {
                City = "Santo Antônio da Patrulha",
                Highway = "BR-290",
                Kms = 19.40M,
                State = "RS",
                Longitude = -29.89M,
                Latitude = -50.45M,
                Concessionaire = default!,
            },
            new()
            {
                City = "Montenegro",
                Highway = "BR-386",
                Kms = 426M,
                State = "RS",
                Longitude = -29.82M,
                Latitude = -51.36M,
                Concessionaire = default!,
            },
        },
        };

        var viaCosteira = new TollConcessionaire
        {
            CompanyName = "VIA COSTEIRA",
            LegalDocument = "05651554000172",
            CreatedAt = DateTime.UtcNow,
            Plazas =
        {
            new()
            {
                City = "Laguna",
                Highway = "BR-101",
                Kms = 298.66M,
                State = "SC",
                Longitude = -48.739429M,
                Latitude = -28.350722M,
                Concessionaire = default!,
            },
        }
        };

        return
        [
            ccrViaSul,
            viaCosteira,
        ];
    }
}