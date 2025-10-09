using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NorthWind.Application.Common;
using NorthWind.Infrastructure.Configuration;
using NorthWind.Infrastructure.Persistence;

namespace NorthWind.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(ConnectionStringNames.NorthWind);

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException($"Connection string '{ConnectionStringNames.NorthWind}' was not found.");
        }

        services.AddDbContext<NorthWindDbContext>(options => options.UseSqlServer(connectionString));
        services.AddScoped<INorthWindDbContext>(provider => provider.GetRequiredService<NorthWindDbContext>());


        return services;
    }
}
