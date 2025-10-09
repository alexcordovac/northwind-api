using Microsoft.AspNetCore.Mvc;
using NorthWind.API.Orders;
using NorthWind.Application.Common.Pagination;
using NorthWind.Application.Orders;

namespace NorthWind.API.Common.Pagination;

public record PaginatedQueryRequest(
    [FromQuery(Name = "page")] int Page = 1,
    [FromQuery(Name = "rows")] int Rows = 25,
    [FromQuery(Name = "offset")] int Offset = 0,
    [FromQuery(Name = "query")] string? Query = null)
{
    public static implicit operator PaginationParameters(PaginatedQueryRequest request)
       => new(request.Page, request.Rows, request.Offset, request.Query);
}
