using FluentResults;
using Microsoft.EntityFrameworkCore;
using NorthWind.Application.Common;
using NorthWind.Application.Common.Pagination;
using NorthWind.Application.Employees.DTOs;
using NorthWind.Application.Employees.Queries;
using NorthWind.Domain.Entities;

namespace NorthWind.Application.Employees;

public sealed class GetEmployeesUseCase
{
    private readonly INorthWindDbContext _dbContext;

    public GetEmployeesUseCase(INorthWindDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<PagedResult<EmployeeSummaryDto>>> ExecuteAsync(
        PaginationParameters pagination,
        CancellationToken cancellationToken)
    {
        IQueryable<Employee> baseQuery = _dbContext
            .Employees
            .WithoutTracking()
            .ApplySearch(pagination.Query)
            .OrderByName();

        int totalRows = await baseQuery.CountAsync(cancellationToken);

        if (totalRows > 0 && pagination.Offset >= totalRows)
        {
            return Result.Fail(new Error("Requested page is outside the available data range."));
        }

        IReadOnlyCollection<EmployeeSummaryDto> employees = totalRows == 0
            ? Array.Empty<EmployeeSummaryDto>()
            : await baseQuery
                .ToPageAsync(pagination, query => query.ProjectToSummary(), cancellationToken);

        PaginationMetadata metadata = PaginationExtensions.CreateMetadata(pagination, totalRows, employees.Count);

        var response = new PagedResult<EmployeeSummaryDto>(employees, metadata);

        return Result.Ok(response);
    }
}
