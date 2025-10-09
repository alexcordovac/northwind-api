using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NorthWind.Domain.Entities;

namespace NorthWind.Infrastructure.Persistence.Configurations;

internal sealed class CustomerCustomerDemoConfiguration : IEntityTypeConfiguration<CustomerCustomerDemo>
{
    public void Configure(EntityTypeBuilder<CustomerCustomerDemo> builder)
    {
        builder.ToTable("CustomerCustomerDemo");

        builder.HasKey(entity => new { entity.CustomerId, entity.CustomerTypeId });

        builder.Property(entity => entity.CustomerId)
            .HasColumnName("CustomerID")
            .HasMaxLength(5)
            .IsFixedLength();

        builder.Property(entity => entity.CustomerTypeId)
            .HasColumnName("CustomerTypeID")
            .HasMaxLength(10)
            .IsFixedLength();

        builder.HasOne(entity => entity.Customer)
            .WithMany(customer => customer.CustomerCustomerDemos)
            .HasForeignKey(entity => entity.CustomerId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_CustomerCustomerDemo_Customers");

        builder.HasOne(entity => entity.CustomerDemographic)
            .WithMany(demographic => demographic.CustomerCustomerDemos)
            .HasForeignKey(entity => entity.CustomerTypeId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_CustomerCustomerDemo");
    }
}
