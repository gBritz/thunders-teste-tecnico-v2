using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Thunders.TechTest.ApiService.Domain.Reports;

namespace Thunders.TechTest.ApiService.Data.Mappings.Reports;

public class ReportConfiguration : IEntityTypeConfiguration<Report>
{
    public void Configure(EntityTypeBuilder<Report> builder)
    {
        builder.ToTable("Report");

        builder.HasKey(_ => _.Id);
        builder.Property(_ => _.Id)
            .ValueGeneratedOnAdd();

        builder.Property(_ => _.CreatedAt)
            .IsRequired();

        builder.Property(_ => _.FinishedAt);

        builder.Property(_ => _.ElapsedExecution);

        builder.Property(_ => _.FileName)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(_ => _.Status)
            .IsRequired();

        builder.Property(_ => _.Type)
            .IsRequired();

        builder.Property(_ => _.FormatType)
            .IsRequired();

        builder.Property(_ => _.ErrorMessage)
            .HasMaxLength(500);

        builder.Property(_ => _.BlobFile);

        builder.Property(_ => _.JsonResultData)
            .HasColumnType("text");

        builder.Property(_ => _.PostbackUrl)
            .HasMaxLength(500);

        builder.HasMany(_ => _.Parameters)
            .WithOne()
            .HasForeignKey("ReportId")
            .OnDelete(DeleteBehavior.Cascade);
    }
}