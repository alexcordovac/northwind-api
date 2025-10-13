using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using NorthWind.Application.Customers;
using NorthWind.Application.Employees;
using NorthWind.Application.Orders;
using NorthWind.Application.Products;

namespace NorthWind.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddTransient<GetCustomersUseCase>();
        services.AddTransient<GetEmployeesUseCase>();
        services.AddTransient<CreateOrderUseCase>();
        services.AddTransient<GetOrdersUseCase>();
        services.AddTransient<DeleteOrderUseCase>();
        services.AddTransient<GetProductsUseCase>();

        return services;
    }
}
