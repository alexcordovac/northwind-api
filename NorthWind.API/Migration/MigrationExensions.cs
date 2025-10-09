using Microsoft.EntityFrameworkCore;
using NorthWind.Infrastructure.Persistence;
using System;

namespace NorthWind.API.Migration
{
    public static class MigrationExtensions
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();

            ApplyMigration<NorthWindDbContext>(scope);
        }

        private static void ApplyMigration<TDbContext>(IServiceScope scope)
            where TDbContext : DbContext
        {
            using TDbContext context = scope.ServiceProvider.GetRequiredService<TDbContext>();

            context.Database.Migrate();
        }
    }
}
