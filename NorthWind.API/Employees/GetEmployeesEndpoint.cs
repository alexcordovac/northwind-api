using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NorthWind.API.Common;
using NorthWind.API.Common.Pagination;
using NorthWind.API.Endpoints;
using NorthWind.API.Middlewares;
using NorthWind.Application.Common.Pagination;
using NorthWind.Application.Employees;
using NorthWind.Application.Employees.DTOs;

namespace NorthWind.API.Employees;

internal sealed class GetEmployeesEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder group)
    {
        group.MapGet(
                EmployeeEndpoints.Endpoint,
                HandleAsync)
            .WithTags(EmployeeEndpoints.Tag)
            .Produces<PagedResult<EmployeeSummaryDto>>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .AddEndpointFilter<ValidationFilter<PaginatedQueryRequest>>();
    }

    private async Task<IResult> HandleAsync(
        [AsParameters] PaginatedQueryRequest request,
        GetEmployeesUseCase useCase,
        CancellationToken cancellationToken)
    {
        var result = await useCase.ExecuteAsync(request, cancellationToken);
        return result.ToHttpResult();
    }
}
