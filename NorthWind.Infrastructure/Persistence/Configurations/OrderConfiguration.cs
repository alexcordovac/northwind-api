using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NorthWind.Domain.Entities;

namespace NorthWind.Infrastructure.Persistence.Configurations;

internal sealed class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");

        builder.HasKey(order => order.OrderId);

        builder.Property(order => order.OrderId)
            .HasColumnName("OrderID");

        builder.Property(order => order.CustomerId)
            .HasColumnName("CustomerID")
            .HasMaxLength(5)
            .IsFixedLength();

        builder.Property(order => order.EmployeeId)
            .HasColumnName("EmployeeID");

        builder.Property(order => order.OrderDate)
            .HasColumnName("OrderDate")
            .HasColumnType("datetime");

        builder.Property(order => order.RequiredDate)
            .HasColumnName("RequiredDate")
            .HasColumnType("datetime");

        builder.Property(order => order.ShippedDate)
            .HasColumnName("ShippedDate")
            .HasColumnType("datetime");

        builder.Property(order => order.ShipVia)
            .HasColumnName("ShipVia");

        builder.Property(order => order.Freight)
            .HasColumnName("Freight")
            .HasColumnType("money")
            .HasDefaultValue(0m);

        builder.Property(order => order.ShipName)
            .HasColumnName("ShipName")
            .HasMaxLength(40);

        builder.Property(order => order.ShipAddress)
            .HasColumnName("ShipAddress")
            .HasMaxLength(60);

        builder.Property(order => order.ShipCity)
            .HasColumnName("ShipCity")
            .HasMaxLength(15);

        builder.Property(order => order.ShipRegion)
            .HasColumnName("ShipRegion")
            .HasMaxLength(15);

        builder.Property(order => order.ShipPostalCode)
            .HasColumnName("ShipPostalCode")
            .HasMaxLength(10);

        builder.Property(order => order.ShipCountry)
            .HasColumnName("ShipCountry")
            .HasMaxLength(15);

        builder.HasIndex(order => order.CustomerId)
            .HasDatabaseName("CustomerID");

        builder.HasIndex(order => order.EmployeeId)
            .HasDatabaseName("EmployeeID");

        builder.HasIndex(order => order.OrderDate)
            .HasDatabaseName("OrderDate");

        builder.HasIndex(order => order.ShippedDate)
            .HasDatabaseName("ShippedDate");

        builder.HasIndex(order => order.ShipPostalCode)
            .HasDatabaseName("ShipPostalCode");

        builder.HasIndex(order => order.ShipVia)
            .HasDatabaseName("ShippersOrders");

        builder.HasOne(order => order.Customer)
            .WithMany(customer => customer.Orders)
            .HasForeignKey(order => order.CustomerId)
            .OnDelete(DeleteBehavior.SetNull)
            .HasConstraintName("FK_Orders_Customers");

        builder.HasOne(order => order.Employee)
            .WithMany(employee => employee.Orders)
            .HasForeignKey(order => order.EmployeeId)
            .OnDelete(DeleteBehavior.SetNull)
            .HasConstraintName("FK_Orders_Employees");

        builder.HasOne(order => order.Shipper)
            .WithMany(shipper => shipper.Orders)
            .HasForeignKey(order => order.ShipVia)
            .OnDelete(DeleteBehavior.SetNull)
            .HasConstraintName("FK_Orders_Shippers");
    }
}
