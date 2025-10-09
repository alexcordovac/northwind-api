using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NorthWind.Domain.Entities;

namespace NorthWind.Infrastructure.Persistence.Configurations;

internal sealed class ShipperConfiguration : IEntityTypeConfiguration<Shipper>
{
    public void Configure(EntityTypeBuilder<Shipper> builder)
    {
        builder.ToTable("Shippers");

        builder.HasKey(shipper => shipper.ShipperId);

        builder.Property(shipper => shipper.ShipperId)
            .HasColumnName("ShipperID");

        builder.Property(shipper => shipper.CompanyName)
            .HasColumnName("CompanyName")
            .HasMaxLength(40);

        builder.Property(shipper => shipper.Phone)
            .HasColumnName("Phone")
            .HasMaxLength(24);
    }
}
