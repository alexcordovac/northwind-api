using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace NorthWind.API.Extensions;

public static class CorsExtensions
{
    private const string AllowedFrontendOrigin = "FrontendClient";

    public static IServiceCollection AddApiCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(AllowedFrontendOrigin, policy =>
            {
                policy.WithOrigins("http://localhost:4200")
                      .AllowAnyHeader()
                      .AllowAnyMethod();
            });
        });

        return services;
    }

    public static WebApplication UseApiCors(this WebApplication app)
    {
        app.UseCors(AllowedFrontendOrigin);
        return app;
    }
}
