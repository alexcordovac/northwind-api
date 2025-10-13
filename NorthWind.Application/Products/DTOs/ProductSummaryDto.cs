namespace NorthWind.Application.Products.DTOs;

public sealed record ProductSummaryDto(
    int ProductId,
    string ProductName,
    int? SupplierId,
    int? CategoryId,
    string? QuantityPerUnit,
    decimal? UnitPrice,
    short? UnitsInStock,
    short? UnitsOnOrder,
    short? ReorderLevel,
    bool Discontinued);
