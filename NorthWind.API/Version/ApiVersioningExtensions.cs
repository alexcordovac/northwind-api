using Asp.Versioning;
using Asp.Versioning.Builder;
using NorthWind.API.Common;

namespace NorthWind.API.Version
{
    internal static class ApiVersioningExtensions
    {
        internal static IServiceCollection AddVersions(this IServiceCollection services)
        {
            services
                .AddApiVersioning(options =>
                {
                    options.DefaultApiVersion = NorthWindApiVersion.V1;
                    options.ApiVersionReader = new UrlSegmentApiVersionReader();
                })
                .AddApiExplorer(options =>
                {
                    options.GroupNameFormat = "'v'V";
                    options.SubstituteApiVersionInUrl = true;
                });

            return services;
        }

        internal static RouteGroupBuilder CreateRouteGorupBuilder(this WebApplication app)
        {
            ApiVersionSet apiVersionSet = app.NewApiVersionSet()
            .HasApiVersion(NorthWindApiVersion.V1)
            //.HasApiVersion(NorthWindApiVersion.V2)
            .ReportApiVersions()
            .Build();

            RouteGroupBuilder group = app
                .MapGroup("api/v{version:apiVersion}")
                .WithApiVersionSet(apiVersionSet);

            return group;
        }
    }
}
