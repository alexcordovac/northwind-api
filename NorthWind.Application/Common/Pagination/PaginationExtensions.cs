using Microsoft.EntityFrameworkCore;

namespace NorthWind.Application.Common.Pagination;

public static class PaginationExtensions
{
    public static IQueryable<T> ApplyPagination<T>(this IQueryable<T> query, PaginationParameters parameters)
    {
        if (parameters.Skip > 0)
        {
            query = query.Skip(parameters.Skip);
        }

        if (parameters.Rows > 0)
        {
            query = query.Take(parameters.Rows);
        }

        return query;
    }

    public static async Task<IReadOnlyCollection<TProjection>> ToPageAsync<TSource, TProjection>(
        this IQueryable<TSource> query,
        PaginationParameters parameters,
        Func<IQueryable<TSource>, IQueryable<TProjection>> projection,
        CancellationToken cancellationToken = default)
    {
        IQueryable<TProjection> pageQuery = projection(query.ApplyPagination(parameters));

        return await pageQuery.ToListAsync(cancellationToken);
    }

    public static PaginationMetadata CreateMetadata(
        PaginationParameters parameters,
        int totalRows,
        int returnedCount)
    {
        int totalPages = totalRows == 0
            ? 0
            : (int)Math.Ceiling(totalRows / (double)parameters.Rows);

        bool hasPrevious = totalRows > 0 && parameters.Skip > 0;
        bool hasNext = totalRows > 0 && parameters.Skip + returnedCount < totalRows;

        return new PaginationMetadata(
            parameters.Page,
            parameters.Rows,
            parameters.Offset,
            parameters.Query,
            totalRows,
            totalPages,
            hasPrevious,
            hasNext);
    }
}
