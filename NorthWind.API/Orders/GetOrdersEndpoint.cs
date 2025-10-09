using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NorthWind.API.Common;
using NorthWind.API.Endpoints;
using NorthWind.Application.Orders;
using NorthWind.Application.Orders.DTOs;

namespace NorthWind.API.Orders;

internal sealed class GetOrdersEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder group)
    {
        group.MapGet(
                OrderEndpoints.Endpoint,
                async ([AsParameters] GetOrdersRequest request, GetOrdersUseCase getOrdersUseCase, CancellationToken cancellationToken) =>
                {
                    var result = await getOrdersUseCase.ExecuteAsync(request.ToInput(), cancellationToken);
                    return result.ToHttpResult();
                })
            .WithTags(OrderEndpoints.Tag)
            .Produces<GetOrdersResponseDto>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}
