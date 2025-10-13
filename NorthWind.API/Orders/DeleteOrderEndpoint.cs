using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NorthWind.API.Common;
using NorthWind.API.Endpoints;
using NorthWind.API.Middlewares;
using NorthWind.Application.Orders;

namespace NorthWind.API.Orders;

internal sealed class DeleteOrderEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder group)
    {
        group.MapDelete($"{OrderEndpoints.Endpoint}/{{orderId:int}}", HandleAsync)
            .WithTags(OrderEndpoints.Tag)
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .AddEndpointFilter<ValidationFilter<DeleteOrderRequest>>();
    }

    private static async Task<IResult> HandleAsync(
        [AsParameters] DeleteOrderRequest request,
        DeleteOrderUseCase useCase,
        CancellationToken cancellationToken)
    {
        var result = await useCase.ExecuteAsync(request.OrderId, cancellationToken);
        return result.ToHttpResult();
    }
}
