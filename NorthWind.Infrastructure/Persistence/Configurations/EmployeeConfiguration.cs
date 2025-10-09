using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NorthWind.Domain.Entities;

namespace NorthWind.Infrastructure.Persistence.Configurations;

internal sealed class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable("Employees");

        builder.HasKey(employee => employee.EmployeeId);

        builder.Property(employee => employee.EmployeeId)
            .HasColumnName("EmployeeID");

        builder.Property(employee => employee.LastName)
            .HasColumnName("LastName")
            .HasMaxLength(20);

        builder.Property(employee => employee.FirstName)
            .HasColumnName("FirstName")
            .HasMaxLength(10);

        builder.Property(employee => employee.Title)
            .HasColumnName("Title")
            .HasMaxLength(30);

        builder.Property(employee => employee.TitleOfCourtesy)
            .HasColumnName("TitleOfCourtesy")
            .HasMaxLength(25);

        builder.Property(employee => employee.BirthDate)
            .HasColumnName("BirthDate")
            .HasColumnType("datetime");

        builder.Property(employee => employee.HireDate)
            .HasColumnName("HireDate")
            .HasColumnType("datetime");

        builder.Property(employee => employee.Address)
            .HasColumnName("Address")
            .HasMaxLength(60);

        builder.Property(employee => employee.City)
            .HasColumnName("City")
            .HasMaxLength(15);

        builder.Property(employee => employee.Region)
            .HasColumnName("Region")
            .HasMaxLength(15);

        builder.Property(employee => employee.PostalCode)
            .HasColumnName("PostalCode")
            .HasMaxLength(10);

        builder.Property(employee => employee.Country)
            .HasColumnName("Country")
            .HasMaxLength(15);

        builder.Property(employee => employee.HomePhone)
            .HasColumnName("HomePhone")
            .HasMaxLength(24);

        builder.Property(employee => employee.Extension)
            .HasColumnName("Extension")
            .HasMaxLength(4);

        builder.Property(employee => employee.Photo)
            .HasColumnName("Photo")
            .HasColumnType("image");

        builder.Property(employee => employee.Notes)
            .HasColumnName("Notes")
            .HasColumnType("ntext");

        builder.Property(employee => employee.ReportsTo)
            .HasColumnName("ReportsTo");

        builder.Property(employee => employee.PhotoPath)
            .HasColumnName("PhotoPath")
            .HasMaxLength(255);

        builder.HasIndex(employee => employee.LastName)
            .HasDatabaseName("LastName");

        builder.HasIndex(employee => employee.PostalCode)
            .HasDatabaseName("PostalCode");

        builder.HasOne(employee => employee.Manager)
            .WithMany(manager => manager.DirectReports)
            .HasForeignKey(employee => employee.ReportsTo)
            .HasConstraintName("FK_Employees_Employees");
    }
}
