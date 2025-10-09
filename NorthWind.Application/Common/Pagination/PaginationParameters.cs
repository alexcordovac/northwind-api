namespace NorthWind.Application.Common.Pagination;

public sealed record PaginationParameters(int Page, int Rows, int Offset, string? Query)
{
    public int Skip => ((Page - 1) * Rows) + Offset;
}
