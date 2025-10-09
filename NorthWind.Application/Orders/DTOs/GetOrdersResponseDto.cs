namespace NorthWind.Application.Orders.DTOs;

public sealed record GetOrdersResponseDto(
    IReadOnlyCollection<OrderSummaryDto> Orders,
    int Page,
    int Rows,
    int Offset,
    string? Query,
    int TotalRows,
    int TotalPages,
    bool HasPrevious,
    bool HasNext);
