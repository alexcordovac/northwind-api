using FluentResults;
using Microsoft.EntityFrameworkCore;
using NorthWind.Application.Common;
using NorthWind.Application.Orders.DTOs;
using NorthWind.Application.Orders.Queries;
using NorthWind.Domain.Entities;

namespace NorthWind.Application.Orders;

public sealed class GetOrdersUseCase(INorthWindDbContext _dbContext)
{
    public async Task<Result<GetOrdersResponseDto>> ExecuteAsync(GetOrdersInput input, CancellationToken cancellationToken)
    {
        string? searchTerm = input.NormalizedQuery;
        int recordsToSkip = ((input.Page - 1) * input.Rows) + input.Offset;

        IQueryable<Order> baseQuery = _dbContext
            .Orders
            .WithoutTracking()
            .ApplySearch(searchTerm)
            .OrderByMostRecent();

        int totalRows = await baseQuery.CountAsync(cancellationToken);

        if (totalRows > 0 && recordsToSkip >= totalRows)
        {
            return Result.Fail(new Error("Requested page is outside the available data range."));
        }

        int totalPages = totalRows == 0
            ? 0
            : (int)Math.Ceiling(totalRows / (double)input.Rows);

        IReadOnlyCollection<OrderSummaryDto> orders = totalRows == 0
            ? Array.Empty<OrderSummaryDto>()
            : await baseQuery
                .ApplyPagination(recordsToSkip, input.Rows)
                .ProjectToSummary()
                .ToListAsync(cancellationToken);

        bool hasPrevious = recordsToSkip > 0 && totalRows > 0;
        bool hasNext = recordsToSkip + orders.Count < totalRows;

        var response = new GetOrdersResponseDto(
            orders,
            input.Page,
            input.Rows,
            input.Offset,
            searchTerm,
            totalRows,
            totalPages,
            hasPrevious,
            hasNext);

        return Result.Ok(response);
    }
}
