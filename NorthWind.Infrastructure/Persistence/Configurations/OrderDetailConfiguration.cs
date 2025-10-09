using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NorthWind.Domain.Entities;

namespace NorthWind.Infrastructure.Persistence.Configurations;

internal sealed class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
{
    public void Configure(EntityTypeBuilder<OrderDetail> builder)
    {
        builder.ToTable("Order Details");

        builder.HasKey(detail => new { detail.OrderId, detail.ProductId });

        builder.Property(detail => detail.OrderId)
            .HasColumnName("OrderID");

        builder.Property(detail => detail.ProductId)
            .HasColumnName("ProductID");

        builder.Property(detail => detail.UnitPrice)
            .HasColumnName("UnitPrice")
            .HasColumnType("money")
            .HasDefaultValue(0m);

        builder.Property(detail => detail.Quantity)
            .HasColumnName("Quantity")
            .HasDefaultValue<short>(1);

        builder.Property(detail => detail.Discount)
            .HasColumnName("Discount")
            .HasColumnType("real")
            .HasDefaultValue(0f);

        builder.HasIndex(detail => detail.OrderId)
            .HasDatabaseName("OrderID");

        builder.HasIndex(detail => detail.ProductId)
            .HasDatabaseName("ProductID");

        builder.HasOne(detail => detail.Order)
            .WithMany(order => order.OrderDetails)
            .HasForeignKey(detail => detail.OrderId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_Order_Details_Orders");

        builder.HasOne(detail => detail.Product)
            .WithMany(product => product.OrderDetails)
            .HasForeignKey(detail => detail.ProductId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_Order_Details_Products");
    }
}
