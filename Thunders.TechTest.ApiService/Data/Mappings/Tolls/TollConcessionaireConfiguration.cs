using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Thunders.TechTest.ApiService.Domain.Tolls;

namespace Thunders.TechTest.ApiService.Data.Mappings.Tolls;

internal class TollConcessionaireConfiguration : IEntityTypeConfiguration<TollConcessionaire>
{
    public void Configure(EntityTypeBuilder<TollConcessionaire> builder)
    {
        builder.ToTable("TollConcessionaire");

        builder.HasKey(_ => _.Id);
        builder.Property(_ => _.Id)
            .ValueGeneratedOnAdd();

        builder.Property(_ => _.CompanyName)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(_ => _.LegalDocument)
            .HasMaxLength(14)
            .IsFixedLength()
            .IsRequired();

        builder.Property(_ => _.CreatedAt)
            .IsRequired();
    }
}