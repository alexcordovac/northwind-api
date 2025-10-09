using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NorthWind.API.Common;
using NorthWind.API.Endpoints;
using NorthWind.API.Filters;
using NorthWind.Application.Orders;
using NorthWind.Application.Orders.DTOs;

namespace NorthWind.API.Orders;

internal sealed class GetOrdersEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder group)
    {
        group.MapGet(OrderEndpoints.Endpoint, HandleAsync)
            .WithTags(OrderEndpoints.Tag)
            .AddEndpointFilter<ValidationFilter<GetOrdersRequest>>()
            .Produces<GetOrdersResponseDto>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest);
    }

    private async Task<IResult> HandleAsync(
        [AsParameters] GetOrdersRequest request,
        GetOrdersUseCase useCase,
        CancellationToken cancellationToken)
    {
        var result = await useCase.ExecuteAsync(request, cancellationToken);

        return result.ToHttpResult();
    }
}
