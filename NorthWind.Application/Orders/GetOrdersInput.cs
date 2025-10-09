using NorthWind.Application.Common.Pagination;

namespace NorthWind.Application.Orders;
public sealed record GetOrdersInput(int Page, int Rows, int Offset, string? Query) : PaginatedRequest(Page, Rows, Offset, Query);