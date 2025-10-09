using Microsoft.Extensions.DependencyInjection;
using NorthWind.Application.Orders;


namespace NorthWind.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient<GetOrdersUseCase>();
            
            return services;
        }
    }
}
