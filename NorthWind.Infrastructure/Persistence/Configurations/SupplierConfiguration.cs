using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NorthWind.Domain.Entities;

namespace NorthWind.Infrastructure.Persistence.Configurations;

internal sealed class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
{
    public void Configure(EntityTypeBuilder<Supplier> builder)
    {
        builder.ToTable("Suppliers");

        builder.HasKey(supplier => supplier.SupplierId);

        builder.Property(supplier => supplier.SupplierId)
            .HasColumnName("SupplierID");

        builder.Property(supplier => supplier.CompanyName)
            .HasColumnName("CompanyName")
            .HasMaxLength(40);

        builder.Property(supplier => supplier.ContactName)
            .HasColumnName("ContactName")
            .HasMaxLength(30);

        builder.Property(supplier => supplier.ContactTitle)
            .HasColumnName("ContactTitle")
            .HasMaxLength(30);

        builder.Property(supplier => supplier.Address)
            .HasColumnName("Address")
            .HasMaxLength(60);

        builder.Property(supplier => supplier.City)
            .HasColumnName("City")
            .HasMaxLength(15);

        builder.Property(supplier => supplier.Region)
            .HasColumnName("Region")
            .HasMaxLength(15);

        builder.Property(supplier => supplier.PostalCode)
            .HasColumnName("PostalCode")
            .HasMaxLength(10);

        builder.Property(supplier => supplier.Country)
            .HasColumnName("Country")
            .HasMaxLength(15);

        builder.Property(supplier => supplier.Phone)
            .HasColumnName("Phone")
            .HasMaxLength(24);

        builder.Property(supplier => supplier.Fax)
            .HasColumnName("Fax")
            .HasMaxLength(24);

        builder.Property(supplier => supplier.HomePage)
            .HasColumnName("HomePage")
            .HasColumnType("ntext");

        builder.HasIndex(supplier => supplier.CompanyName)
            .HasDatabaseName("CompanyName");

        builder.HasIndex(supplier => supplier.PostalCode)
            .HasDatabaseName("PostalCode");
    }
}
