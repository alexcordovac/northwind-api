using FluentResults;
using Microsoft.EntityFrameworkCore;
using NorthWind.Application.Common;
using NorthWind.Application.Common.Pagination;
using NorthWind.Application.Customers.DTOs;
using NorthWind.Application.Customers.Queries;
using NorthWind.Domain.Entities;

namespace NorthWind.Application.Customers;

public sealed class GetCustomersUseCase
{
    private readonly INorthWindDbContext _dbContext;

    public GetCustomersUseCase(INorthWindDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<PagedResult<CustomerSummaryDto>>> ExecuteAsync(
        PaginationParameters pagination,
        CancellationToken cancellationToken)
    {
        IQueryable<Customer> baseQuery = _dbContext
            .Customers
            .WithoutTracking()
            .ApplySearch(pagination.Query)
            .OrderByCompany();

        int totalRows = await baseQuery.CountAsync(cancellationToken);

        if (totalRows > 0 && pagination.Offset >= totalRows)
        {
            return Result.Fail(new Error("Requested page is outside the available data range."));
        }

        IReadOnlyCollection<CustomerSummaryDto> customers = totalRows == 0
            ? Array.Empty<CustomerSummaryDto>()
            : await baseQuery
                .ToPageAsync(pagination, query => query.ProjectToSummary(), cancellationToken);

        PaginationMetadata metadata = PaginationExtensions.CreateMetadata(pagination, totalRows, customers.Count);

        var response = new PagedResult<CustomerSummaryDto>(customers, metadata);

        return Result.Ok(response);
    }
}
