using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NorthWind.Domain.Entities;

namespace NorthWind.Infrastructure.Persistence.Configurations;

internal sealed class CustomerDemographicConfiguration : IEntityTypeConfiguration<CustomerDemographic>
{
    public void Configure(EntityTypeBuilder<CustomerDemographic> builder)
    {
        builder.ToTable("CustomerDemographics");

        builder.HasKey(demographic => demographic.CustomerTypeId);

        builder.Property(demographic => demographic.CustomerTypeId)
            .HasColumnName("CustomerTypeID")
            .HasMaxLength(10)
            .IsFixedLength();

        builder.Property(demographic => demographic.CustomerDesc)
            .HasColumnName("CustomerDesc")
            .HasColumnType("ntext");
    }
}
