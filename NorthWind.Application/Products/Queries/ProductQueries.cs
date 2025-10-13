using Microsoft.EntityFrameworkCore;
using NorthWind.Application.Products.DTOs;
using NorthWind.Domain.Entities;

namespace NorthWind.Application.Products.Queries;

public static class ProductQueries
{
    public static IQueryable<Product> WithoutTracking(this IQueryable<Product> query) =>
        query.AsNoTracking();

    public static IQueryable<Product> ApplySearch(this IQueryable<Product> query, string? searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            return query;
        }

        string pattern = $"%{searchTerm.Trim()}%";

        return query.Where(product =>
            (product.ProductName != null && EF.Functions.Like(product.ProductName, pattern)) ||
            (product.QuantityPerUnit != null && EF.Functions.Like(product.QuantityPerUnit, pattern)));
    }

    public static IQueryable<Product> OrderByName(this IQueryable<Product> query) =>
        query.OrderBy(product => product.ProductName)
             .ThenBy(product => product.ProductId);

    public static IQueryable<ProductSummaryDto> ProjectToSummary(this IQueryable<Product> query) =>
        query.Select(product => new ProductSummaryDto(
            product.ProductId,
            product.ProductName ?? string.Empty,
            product.SupplierId,
            product.CategoryId,
            product.QuantityPerUnit,
            product.UnitPrice,
            product.UnitsInStock,
            product.UnitsOnOrder,
            product.ReorderLevel,
            product.Discontinued));
}
