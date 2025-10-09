namespace NorthWind.Application.Common.Pagination;

public sealed record PaginationMetadata(
    int Page,
    int Rows,
    int Offset,
    string? Query,
    int TotalRows,
    int TotalPages,
    bool HasPrevious,
    bool HasNext);
