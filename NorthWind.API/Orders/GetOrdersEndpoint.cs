using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NorthWind.API.Common;
using NorthWind.API.Common.Pagination;
using NorthWind.API.Endpoints;
using NorthWind.API.Middlewares;
using NorthWind.Application.Common.Pagination;
using NorthWind.Application.Orders;
using NorthWind.Application.Orders.DTOs;
using NorthWind.API.Security;

namespace NorthWind.API.Orders;

internal sealed class GetOrdersEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder group)
    {
        group.MapGet(
                OrderEndpoints.Endpoint,
                HandleAsync)
            .WithTags(OrderEndpoints.Tag)
            .Produces<PagedResult<OrderSummaryDto>>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .AddEndpointFilter<ValidationFilter<PaginatedQueryRequest>>()
            .RequireAuthorization(AuthorizationPolicyNames.OrderView)
            .WithOpenApi();
    }

    private static async Task<IResult> HandleAsync(
        [AsParameters] PaginatedQueryRequest request,
        GetOrdersUseCase useCase,
        CancellationToken cancellationToken)
    {
        var result = await useCase.ExecuteAsync(request, cancellationToken);
        return result.ToHttpResult();
    }
}
