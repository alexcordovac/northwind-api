using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NorthWind.API.Common;
using NorthWind.API.Endpoints;
using NorthWind.API.Middlewares;
using NorthWind.Application.Orders;
using NorthWind.Application.Orders.DTOs;

namespace NorthWind.API.Orders;

internal sealed class CreateOrderEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder group)
    {
        group.MapPost(OrderEndpoints.Endpoint, HandleAsync)
            .WithTags(OrderEndpoints.Tag)
            .Produces<OrderSummaryDto>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .AddEndpointFilter<ValidationFilter<CreateOrderRequest>>();
    }

    private static async Task<IResult> HandleAsync(
        [FromBody] CreateOrderRequest request,
        CreateOrderUseCase useCase,
        CancellationToken cancellationToken)
    {
        var result = await useCase.ExecuteAsync(request, cancellationToken);
        return result.ToHttpResult(summary => TypedResults.Created($"/{OrderEndpoints.Endpoint}/{summary.OrderId}", summary));
    }
}
