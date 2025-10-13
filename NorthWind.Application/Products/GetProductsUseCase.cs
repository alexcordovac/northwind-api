using FluentResults;
using Microsoft.EntityFrameworkCore;
using NorthWind.Application.Common;
using NorthWind.Application.Common.Pagination;
using NorthWind.Application.Products.DTOs;
using NorthWind.Application.Products.Queries;
using NorthWind.Domain.Entities;

namespace NorthWind.Application.Products;

public sealed class GetProductsUseCase
{
    private readonly INorthWindDbContext _dbContext;

    public GetProductsUseCase(INorthWindDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<PagedResult<ProductSummaryDto>>> ExecuteAsync(
        PaginationParameters pagination,
        CancellationToken cancellationToken)
    {
        IQueryable<Product> baseQuery = _dbContext
            .Products
            .WithoutTracking()
            .ApplySearch(pagination.Query)
            .OrderByName();

        int totalRows = await baseQuery.CountAsync(cancellationToken);

        if (totalRows > 0 && pagination.Offset >= totalRows)
        {
            return Result.Fail(new Error("Requested page is outside the available data range."));
        }

        IReadOnlyCollection<ProductSummaryDto> products = totalRows == 0
            ? Array.Empty<ProductSummaryDto>()
            : await baseQuery
                .ToPageAsync(pagination, query => query.ProjectToSummary(), cancellationToken);

        PaginationMetadata metadata = PaginationExtensions.CreateMetadata(pagination, totalRows, products.Count);

        var response = new PagedResult<ProductSummaryDto>(products, metadata);

        return Result.Ok(response);
    }
}
