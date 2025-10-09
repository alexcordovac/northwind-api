using NorthWind.API.Common;
using NorthWind.API.Endpoints;
using NorthWind.Application.Orders;

namespace NorthWind.API.Orders
{
    internal sealed class GetOrdersEndpoint(GetOrdersUseCase getOrdersUseCase) : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder group)
        {
            group.MapGet(OrderEndpoints.Endpoint, async () =>
            {

                //Return paginated orders
            })
            .WithTags(OrderEndpoints.Tag);
        }
    }
}
