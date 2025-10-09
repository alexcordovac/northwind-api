namespace NorthWind.Application.Common.Pagination;

public sealed record PagedResult<T>(IReadOnlyCollection<T> Items, PaginationMetadata Metadata);
