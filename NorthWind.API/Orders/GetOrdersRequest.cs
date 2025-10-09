using Microsoft.AspNetCore.Mvc;
using NorthWind.Application.Orders;

namespace NorthWind.API.Orders;

public sealed record GetOrdersRequest(
    [FromQuery(Name = "page")] int Page = 1,
    [FromQuery(Name = "rows")] int Rows = 25,
    [FromQuery(Name = "offset")] int Offset = 0,
    [FromQuery(Name = "query")] string? Query = null)
{
    public static implicit operator GetOrdersInput(GetOrdersRequest request)
        => new(request.Page, request.Rows, request.Offset, request.Query);
}
