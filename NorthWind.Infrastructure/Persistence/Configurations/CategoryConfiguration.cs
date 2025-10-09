using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NorthWind.Domain.Entities;

namespace NorthWind.Infrastructure.Persistence.Configurations;

internal sealed class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories");

        builder.HasKey(category => category.CategoryId);

        builder.Property(category => category.CategoryId)
            .HasColumnName("CategoryID");

        builder.Property(category => category.CategoryName)
            .HasColumnName("CategoryName")
            .HasMaxLength(15);

        builder.Property(category => category.Description)
            .HasColumnName("Description")
            .HasColumnType("ntext");

        builder.Property(category => category.Picture)
            .HasColumnName("Picture")
            .HasColumnType("image");

        builder.HasIndex(category => category.CategoryName)
            .HasDatabaseName("CategoryName");
    }
}
