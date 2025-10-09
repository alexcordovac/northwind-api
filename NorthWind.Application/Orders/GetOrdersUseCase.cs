using FluentResults;
using Microsoft.EntityFrameworkCore;
using NorthWind.Application.Common;
using NorthWind.Application.Common.Pagination;
using NorthWind.Application.Orders.DTOs;
using NorthWind.Application.Orders.Queries;
using NorthWind.Domain.Entities;

namespace NorthWind.Application.Orders;

public sealed class GetOrdersUseCase
{
    private readonly INorthWindDbContext _dbContext;

    public GetOrdersUseCase(INorthWindDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<PagedResult<OrderSummaryDto>>> ExecuteAsync(PaginationParameters pagination, CancellationToken cancellationToken)
    {
        IQueryable<Order> baseQuery = _dbContext
            .Orders
            .WithoutTracking()
            .ApplySearch(pagination.Query)
            .OrderByMostRecent();

        int totalRows = await baseQuery.CountAsync(cancellationToken);

        if (totalRows > 0 && pagination.Skip >= totalRows)
        {
            return Result.Fail(new Error("Requested page is outside the available data range."));
        }

        IReadOnlyCollection<OrderSummaryDto> orders = totalRows == 0
            ? Array.Empty<OrderSummaryDto>()
            : await baseQuery
                .ToPageAsync(pagination, query => query.ProjectToSummary(), cancellationToken);

        PaginationMetadata metadata = PaginationExtensions.CreateMetadata(pagination, totalRows, orders.Count);

        var response = new PagedResult<OrderSummaryDto>(orders, metadata);

        return Result.Ok(response);
    }
}
