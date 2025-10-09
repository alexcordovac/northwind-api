using Microsoft.EntityFrameworkCore;
using NorthWind.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWind.Application.Common
{
    public interface INorthWindDbContext
    {
        DbSet<Order> Orders { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
