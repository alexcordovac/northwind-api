using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NorthWind.Domain.Entities;

namespace NorthWind.Infrastructure.Persistence.Configurations;

internal sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(product => product.ProductId);

        builder.Property(product => product.ProductId)
            .HasColumnName("ProductID");

        builder.Property(product => product.ProductName)
            .HasColumnName("ProductName")
            .HasMaxLength(40);

        builder.Property(product => product.SupplierId)
            .HasColumnName("SupplierID");

        builder.Property(product => product.CategoryId)
            .HasColumnName("CategoryID");

        builder.Property(product => product.QuantityPerUnit)
            .HasColumnName("QuantityPerUnit")
            .HasMaxLength(20);

        builder.Property(product => product.UnitPrice)
            .HasColumnName("UnitPrice")
            .HasColumnType("money")
            .HasDefaultValue(0m);

        builder.Property(product => product.UnitsInStock)
            .HasColumnName("UnitsInStock")
            .HasDefaultValueSql("((0))");

        builder.Property(product => product.UnitsOnOrder)
            .HasColumnName("UnitsOnOrder")
            .HasDefaultValueSql("((0))");

        builder.Property(product => product.ReorderLevel)
            .HasColumnName("ReorderLevel")
            .HasDefaultValueSql("((0))");

        builder.Property(product => product.Discontinued)
            .HasColumnName("Discontinued")
            .HasDefaultValue(false);

        builder.HasIndex(product => product.CategoryId)
            .HasDatabaseName("CategoryID");

        builder.HasIndex(product => product.ProductName)
            .HasDatabaseName("ProductName");

        builder.HasIndex(product => product.SupplierId)
            .HasDatabaseName("SupplierID");

        builder.HasOne(product => product.Category)
            .WithMany(category => category.Products)
            .HasForeignKey(product => product.CategoryId)
            .OnDelete(DeleteBehavior.SetNull)
            .HasConstraintName("FK_Products_Categories");

        builder.HasOne(product => product.Supplier)
            .WithMany(supplier => supplier.Products)
            .HasForeignKey(product => product.SupplierId)
            .OnDelete(DeleteBehavior.SetNull)
            .HasConstraintName("FK_Products_Suppliers");
    }
}
