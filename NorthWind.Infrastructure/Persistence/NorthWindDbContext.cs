using Microsoft.EntityFrameworkCore;
using NorthWind.Application.Common;
using NorthWind.Domain.Entities;

namespace NorthWind.Infrastructure.Persistence;

public class NorthWindDbContext(DbContextOptions<NorthWindDbContext> options) : DbContext(options), INorthWindDbContext
{
    public DbSet<Category> Categories => Set<Category>();

    public DbSet<Customer> Customers => Set<Customer>();

    public DbSet<CustomerCustomerDemo> CustomerCustomerDemos => Set<CustomerCustomerDemo>();

    public DbSet<CustomerDemographic> CustomerDemographics => Set<CustomerDemographic>();

    public DbSet<Employee> Employees => Set<Employee>();

    public DbSet<EmployeeTerritory> EmployeeTerritories => Set<EmployeeTerritory>();

    public DbSet<Order> Orders => Set<Order>();

    public DbSet<OrderDetail> OrderDetails => Set<OrderDetail>();

    public DbSet<Product> Products => Set<Product>();

    public DbSet<Region> Regions => Set<Region>();

    public DbSet<Shipper> Shippers => Set<Shipper>();

    public DbSet<Supplier> Suppliers => Set<Supplier>();

    public DbSet<Territory> Territories => Set<Territory>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(NorthWindDbContext).Assembly);
    }
}
