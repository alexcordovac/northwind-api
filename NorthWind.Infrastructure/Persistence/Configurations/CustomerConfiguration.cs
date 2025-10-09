using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NorthWind.Domain.Entities;

namespace NorthWind.Infrastructure.Persistence.Configurations;

internal sealed class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customers");

        builder.HasKey(customer => customer.CustomerId);

        builder.Property(customer => customer.CustomerId)
            .HasColumnName("CustomerID")
            .HasMaxLength(5)
            .IsFixedLength();

        builder.Property(customer => customer.CompanyName)
            .HasColumnName("CompanyName")
            .HasMaxLength(40);

        builder.Property(customer => customer.ContactName)
            .HasColumnName("ContactName")
            .HasMaxLength(30);

        builder.Property(customer => customer.ContactTitle)
            .HasColumnName("ContactTitle")
            .HasMaxLength(30);

        builder.Property(customer => customer.Address)
            .HasColumnName("Address")
            .HasMaxLength(60);

        builder.Property(customer => customer.City)
            .HasColumnName("City")
            .HasMaxLength(15);

        builder.Property(customer => customer.Region)
            .HasColumnName("Region")
            .HasMaxLength(15);

        builder.Property(customer => customer.PostalCode)
            .HasColumnName("PostalCode")
            .HasMaxLength(10);

        builder.Property(customer => customer.Country)
            .HasColumnName("Country")
            .HasMaxLength(15);

        builder.Property(customer => customer.Phone)
            .HasColumnName("Phone")
            .HasMaxLength(24);

        builder.Property(customer => customer.Fax)
            .HasColumnName("Fax")
            .HasMaxLength(24);

        builder.HasIndex(customer => customer.City)
            .HasDatabaseName("City");

        builder.HasIndex(customer => customer.CompanyName)
            .HasDatabaseName("CompanyName");

        builder.HasIndex(customer => customer.PostalCode)
            .HasDatabaseName("PostalCode");

        builder.HasIndex(customer => customer.Region)
            .HasDatabaseName("Region");
    }
}
