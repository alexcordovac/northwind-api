using System;
using System.Collections.Generic;

namespace NorthWind.Application.Orders;

public sealed record CreateOrderInput(
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
    IReadOnlyCollection<CreateOrderDetailInput> Details);
