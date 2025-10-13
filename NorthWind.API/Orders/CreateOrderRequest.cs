using NorthWind.Application.Orders;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NorthWind.API.Orders;

public sealed record CreateOrderRequest(
    string? CustomerId,
    int? EmployeeId,
    DateTime? OrderDate,
    DateTime? RequiredDate,
    DateTime? ShippedDate,
    int? ShipVia,
    decimal? Freight,
    string? ShipName,
    string? ShipAddress,
    string? ShipCity,
    string? ShipRegion,
    string? ShipPostalCode,
    string? ShipCountry,
    IReadOnlyCollection<CreateOrderDetailRequest> Details)
{
    public static implicit operator CreateOrderInput(CreateOrderRequest request)
       => new CreateOrderInput(
            request.CustomerId,
            request.EmployeeId,
            request.OrderDate,
            request.RequiredDate,
            request.ShippedDate,
            request.ShipVia,
            request.Freight,
            request.ShipName,
            request.ShipAddress,
            request.ShipCity,
            request.ShipRegion,
            request.ShipPostalCode,
            request.ShipCountry,
            request.Details.ToCreateOrderDetailInput());
}

public sealed record CreateOrderDetailRequest(
    int ProductId,
    decimal UnitPrice,
    short Quantity,
    float Discount);

internal static class CreateOrderDetailRequestExtensions
{
    public static CreateOrderDetailInput ToCreateOrderDetailInput(this CreateOrderDetailRequest detail)
    {
        return new CreateOrderDetailInput(
              detail.ProductId,
              detail.UnitPrice,
              detail.Quantity,
              detail.Discount);
    }

    public static IReadOnlyCollection<CreateOrderDetailInput> ToCreateOrderDetailInput(this IReadOnlyCollection<CreateOrderDetailRequest> request)
    {
        return request.Select(detail => detail.ToCreateOrderDetailInput()).ToList().AsReadOnly();
    }
}


