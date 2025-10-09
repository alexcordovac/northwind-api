namespace NorthWind.Application.Common.Pagination;

public abstract record PaginatedRequest(int Page = 1, int Rows = 25, int Offset = 0, string? Query = null)
{
    public string? NormalizedQuery => string.IsNullOrWhiteSpace(Query) ? null : Query.Trim();

    public static implicit operator PaginationParameters(PaginatedRequest request) => new(request.Page, request.Rows, request.Offset, request.NormalizedQuery);

}
