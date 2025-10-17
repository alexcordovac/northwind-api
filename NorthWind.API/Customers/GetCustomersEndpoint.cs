using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NorthWind.API.Common;
using NorthWind.API.Common.Pagination;
using NorthWind.API.Endpoints;
using NorthWind.API.Middlewares;
using NorthWind.Application.Common.Pagination;
using NorthWind.Application.Customers;
using NorthWind.Application.Customers.DTOs;
using NorthWind.API.Security;

namespace NorthWind.API.Customers;

internal sealed class GetCustomersEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder group)
    {
        group.MapGet(
                CustomerEndpoints.Endpoint,
                HandleAsync)
            .WithTags(CustomerEndpoints.Tag)
            .Produces<PagedResult<CustomerSummaryDto>>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .AddEndpointFilter<ValidationFilter<PaginatedQueryRequest>>()
            .RequireAuthorization(AuthorizationPolicyNames.CustomerView);
    }

    private async Task<IResult> HandleAsync(
        [AsParameters] PaginatedQueryRequest request,
        GetCustomersUseCase useCase,
        CancellationToken cancellationToken)
    {
        var result = await useCase.ExecuteAsync(request, cancellationToken);
        return result.ToHttpResult();
    }
}
