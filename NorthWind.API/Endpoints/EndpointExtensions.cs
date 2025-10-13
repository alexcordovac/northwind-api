using Asp.Versioning;
using Asp.Versioning.Builder;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NorthWind.API.Common;
using NorthWind.API.Customers;
using NorthWind.API.Employees;
using NorthWind.API.Orders;
using NorthWind.API.Products;
using System.Reflection;

namespace NorthWind.API.Endpoints;

internal static class EndpointExtensions
{
    internal static IServiceCollection AddEndpoints(this IServiceCollection services)
    {
        services.AddScoped<GetCustomersEndpoint>();
        services.AddScoped<GetEmployeesEndpoint>();
        services.AddScoped<CreateOrderEndpoint>();
        services.AddScoped<DeleteOrderEndpoint>();
        services.AddScoped<GetOrdersEndpoint>();
        services.AddScoped<GetProductsEndpoint>();


        services.AddEndpoints(Assembly.GetExecutingAssembly());

        return services;
    }

    private static IServiceCollection AddEndpoints(this IServiceCollection services, Assembly assembly)
    {
        ServiceDescriptor[] serviceDescriptors = assembly
            .DefinedTypes
            .Where(type => type is { IsAbstract: false, IsInterface: false } &&
                           type.IsAssignableTo(typeof(IEndpoint)))
            .Select(type => ServiceDescriptor.Transient(typeof(IEndpoint), type))
            .ToArray();

        services.TryAddEnumerable(serviceDescriptors);

        return services;
    }

    internal static IApplicationBuilder MapEndpoints(this WebApplication app, RouteGroupBuilder? routeGroupBuilder = null)
    {
        IEnumerable<IEndpoint> endpoints = app.Services.GetRequiredService<IEnumerable<IEndpoint>>();

        IEndpointRouteBuilder builder = routeGroupBuilder is null ? app : routeGroupBuilder;

        foreach (IEndpoint endpoint in endpoints)
        {
            endpoint.MapEndpoint(builder);
        }

        return app;
    }
}
