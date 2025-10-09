using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NorthWind.Domain.Entities;

namespace NorthWind.Infrastructure.Persistence.Configurations;

internal sealed class EmployeeTerritoryConfiguration : IEntityTypeConfiguration<EmployeeTerritory>
{
    public void Configure(EntityTypeBuilder<EmployeeTerritory> builder)
    {
        builder.ToTable("EmployeeTerritories");

        builder.HasKey(entity => new { entity.EmployeeId, entity.TerritoryId });

        builder.Property(entity => entity.EmployeeId)
            .HasColumnName("EmployeeID");

        builder.Property(entity => entity.TerritoryId)
            .HasColumnName("TerritoryID")
            .HasMaxLength(20);

        builder.HasOne(entity => entity.Employee)
            .WithMany(employee => employee.EmployeeTerritories)
            .HasForeignKey(entity => entity.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_EmployeeTerritories_Employees");

        builder.HasOne(entity => entity.Territory)
            .WithMany(territory => territory.EmployeeTerritories)
            .HasForeignKey(entity => entity.TerritoryId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_EmployeeTerritories_Territories");
    }
}
