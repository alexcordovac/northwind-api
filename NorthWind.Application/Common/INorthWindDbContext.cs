using Microsoft.EntityFrameworkCore;
using NorthWind.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace NorthWind.Application.Common;

public interface INorthWindDbContext
{
    DbSet<Customer> Customers { get; }

    DbSet<Employee> Employees { get; }

    DbSet<Order> Orders { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
