using Microsoft.AspNetCore.Mvc;

namespace NorthWind.API.Orders;

public sealed record DeleteOrderRequest(
    [FromRoute(Name = "orderId")] int OrderId);
