using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NorthWind.Domain.Entities;

namespace NorthWind.Infrastructure.Persistence.Configurations;

internal sealed class TerritoryConfiguration : IEntityTypeConfiguration<Territory>
{
    public void Configure(EntityTypeBuilder<Territory> builder)
    {
        builder.ToTable("Territories");

        builder.HasKey(territory => territory.TerritoryId);

        builder.Property(territory => territory.TerritoryId)
            .HasColumnName("TerritoryID")
            .HasMaxLength(20);

        builder.Property(territory => territory.TerritoryDescription)
            .HasColumnName("TerritoryDescription")
            .HasMaxLength(50)
            .IsFixedLength();

        builder.Property(territory => territory.RegionId)
            .HasColumnName("RegionID");

        builder.HasOne(territory => territory.Region)
            .WithMany(region => region.Territories)
            .HasForeignKey(territory => territory.RegionId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_Territories_Region");
    }
}
