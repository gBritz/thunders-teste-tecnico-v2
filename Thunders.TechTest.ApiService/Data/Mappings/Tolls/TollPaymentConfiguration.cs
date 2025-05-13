using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Thunders.TechTest.ApiService.Domain.Tolls;

namespace Thunders.TechTest.ApiService.Data.Mappings;

internal class TollPaymentConfiguration : IEntityTypeConfiguration<TollPayment>
{
    public void Configure(EntityTypeBuilder<TollPayment> builder)
    {
        builder.ToTable("TollPayment");

        builder.HasKey(_ => _.Id);
        builder.Property(_ => _.Id)
            .ValueGeneratedOnAdd();

        builder.Property(_ => _.CreatedAt)
            .IsRequired();

        builder.Property(_ => _.PaidAt)
            .IsRequired();

        builder.Property(_ => _.Amount)
            .HasPrecision(6, 2)
            .IsRequired();

        builder.Property(_ => _.Vehicle)
            .IsRequired();

        builder.HasOne(_ => _.Plaza)
            .WithMany()
            .HasForeignKey("PlazaId")
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();
    }
}