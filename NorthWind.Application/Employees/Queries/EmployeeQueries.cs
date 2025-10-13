using Microsoft.EntityFrameworkCore;
using NorthWind.Application.Employees.DTOs;
using NorthWind.Domain.Entities;

namespace NorthWind.Application.Employees.Queries;

public static class EmployeeQueries
{
    public static IQueryable<Employee> WithoutTracking(this IQueryable<Employee> query) =>
        query.AsNoTracking();

    public static IQueryable<Employee> ApplySearch(this IQueryable<Employee> query, string? searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            return query;
        }

        string pattern = $"%{searchTerm.Trim()}%";

        return query.Where(employee =>
            (employee.FirstName != null && EF.Functions.Like(employee.FirstName, pattern)) ||
            (employee.LastName != null && EF.Functions.Like(employee.LastName, pattern)) ||
            (employee.Title != null && EF.Functions.Like(employee.Title, pattern)) ||
            (employee.City != null && EF.Functions.Like(employee.City, pattern)) ||
            (employee.Country != null && EF.Functions.Like(employee.Country, pattern)));
    }

    public static IQueryable<Employee> OrderByName(this IQueryable<Employee> query) =>
        query.OrderBy(employee => employee.LastName)
             .ThenBy(employee => employee.FirstName)
             .ThenBy(employee => employee.EmployeeId);

    public static IQueryable<EmployeeSummaryDto> ProjectToSummary(this IQueryable<Employee> query) =>
        query.Select(employee => new EmployeeSummaryDto(
            employee.EmployeeId,
            employee.FirstName,
            employee.LastName,
            employee.Title,
            employee.TitleOfCourtesy,
            employee.BirthDate,
            employee.HireDate,
            employee.City,
            employee.Country,
            employee.HomePhone,
            employee.ReportsTo));
}
