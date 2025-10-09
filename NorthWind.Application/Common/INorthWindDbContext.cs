using Microsoft.EntityFrameworkCore;
using NorthWind.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace NorthWind.Application.Common;

public interface INorthWindDbContext
{
    DbSet<Order> Orders { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
