namespace NorthWind.Application.Orders.DTOs;

public sealed record OrderSummaryDto(
    int OrderId,
    string? CustomerId,
    string? CustomerCompanyName,
    DateTime? OrderDate,
    DateTime? RequiredDate,
    DateTime? ShippedDate,
    decimal? Freight,
    string? ShipName,
    string? ShipCity,
    string? ShipCountry);
