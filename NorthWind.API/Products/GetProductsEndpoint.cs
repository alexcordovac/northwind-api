using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NorthWind.API.Common;
using NorthWind.API.Common.Pagination;
using NorthWind.API.Endpoints;
using NorthWind.API.Middlewares;
using NorthWind.Application.Common.Pagination;
using NorthWind.Application.Products;
using NorthWind.Application.Products.DTOs;
using NorthWind.API.Security;

namespace NorthWind.API.Products;

internal sealed class GetProductsEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder group)
    {
        group.MapGet(
                ProductEndpoints.Endpoint,
                HandleAsync)
            .WithTags(ProductEndpoints.Tag)
            .Produces<PagedResult<ProductSummaryDto>>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .AddEndpointFilter<ValidationFilter<PaginatedQueryRequest>>()
            .RequireAuthorization(AuthorizationPolicyNames.ProductView);
    }

    private static async Task<IResult> HandleAsync(
        [AsParameters] PaginatedQueryRequest request,
        GetProductsUseCase useCase,
        CancellationToken cancellationToken)
    {
        var result = await useCase.ExecuteAsync(request, cancellationToken);
        return result.ToHttpResult();
    }
}

