using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NorthWind.Domain.Entities;

namespace NorthWind.Infrastructure.Persistence.Configurations;

internal sealed class RegionConfiguration : IEntityTypeConfiguration<Region>
{
    public void Configure(EntityTypeBuilder<Region> builder)
    {
        builder.ToTable("Region");

        builder.HasKey(region => region.RegionId);

        builder.Property(region => region.RegionId)
            .HasColumnName("RegionID")
            .ValueGeneratedNever();

        builder.Property(region => region.RegionDescription)
            .HasColumnName("RegionDescription")
            .HasMaxLength(50)
            .IsFixedLength();
    }
}
