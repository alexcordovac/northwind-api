using System;

namespace NorthWind.Domain.Entities;

public partial class Order
{
    public static Order Create(
        string? customerId,
        int? employeeId,
        DateTime? orderDate,
        DateTime? requiredDate,
        DateTime? shippedDate,
        int? shipVia,
        decimal? freight,
        string? shipName,
        string? shipAddress,
        string? shipCity,
        string? shipRegion,
        string? shipPostalCode,
        string? shipCountry)
    {
        return new Order
        {
            CustomerId = customerId,
            EmployeeId = employeeId,
            OrderDate = orderDate ?? DateTime.UtcNow,
            RequiredDate = requiredDate,
            ShippedDate = shippedDate,
            ShipVia = shipVia,
            Freight = freight,
            ShipName = shipName,
            ShipAddress = shipAddress,
            ShipCity = shipCity,
            ShipRegion = shipRegion,
            ShipPostalCode = shipPostalCode,
            ShipCountry = shipCountry
        };
    }

    public void AddDetail(int productId, decimal unitPrice, short quantity, float discount)
    {
        OrderDetails.Add(new OrderDetail(default, productId, unitPrice, quantity, discount));
    }
}
