using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Thunders.TechTest.ApiService.Domain.Tolls;

namespace Thunders.TechTest.ApiService.Data.Mappings;

internal class TollPlazaConfiguration : IEntityTypeConfiguration<TollPlaza>
{
    public void Configure(EntityTypeBuilder<TollPlaza> builder)
    {
        builder.ToTable("TollPlaza");

        builder.HasKey(_ => _.Id);
        builder.Property(_ => _.Id)
            .ValueGeneratedOnAdd();

        builder.Property(_ => _.Highway)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(_ => _.Kms)
            .HasPrecision(8, 2)
            .IsRequired();

        builder.Property(_ => _.State)
            .HasMaxLength(2)
            .IsFixedLength()
            .IsRequired();

        builder.Property(_ => _.City)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(_ => _.Latitude)
            .HasPrecision(7, 5)
            .IsRequired();

        builder.Property(_ => _.Longitude)
            .HasPrecision(7, 5)
            .IsRequired();

        builder.Property(_ => _.CreatedAt)
            .IsRequired();

        builder.HasOne(_ => _.Concessionaire)
            .WithMany(_ => _.Plazas)
            .HasForeignKey("ConcessionaireId");

        builder.HasMany(_ => _.Payments)
            .WithOne(_ => _.Plaza)
            .OnDelete(DeleteBehavior.Restrict);
    }
}