namespace NorthWind.Application.Orders;

public sealed record CreateOrderDetailInput(
    int ProductId,
    decimal UnitPrice,
    short Quantity,
    float Discount);