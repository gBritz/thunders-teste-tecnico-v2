using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Thunders.TechTest.ApiService.Domain.Reports;

namespace Thunders.TechTest.ApiService.Data.Mappings.Reports;

public class ReportParameterConfiguration : IEntityTypeConfiguration<ReportParameter>
{
    public void Configure(EntityTypeBuilder<ReportParameter> builder)
    {
        builder.ToTable("ReportParameter");

        builder.HasKey(_ => _.Id);
        builder.Property(_ => _.Id)
            .ValueGeneratedOnAdd();

        builder.Property(_ => _.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(_ => _.Value)
            .HasMaxLength(300)
            .IsRequired();
    }
}