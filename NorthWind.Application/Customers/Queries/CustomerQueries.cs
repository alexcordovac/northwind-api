using Microsoft.EntityFrameworkCore;
using NorthWind.Application.Customers.DTOs;
using NorthWind.Domain.Entities;

namespace NorthWind.Application.Customers.Queries;

public static class CustomerQueries
{
    public static IQueryable<Customer> WithoutTracking(this IQueryable<Customer> query) =>
        query.AsNoTracking();

    public static IQueryable<Customer> ApplySearch(this IQueryable<Customer> query, string? searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            return query;
        }

        string pattern = $"%{searchTerm.Trim()}%";

        return query.Where(customer =>
            (customer.CompanyName != null && EF.Functions.Like(customer.CompanyName, pattern)) ||
            (customer.ContactName != null && EF.Functions.Like(customer.ContactName, pattern)) ||
            (customer.City != null && EF.Functions.Like(customer.City, pattern)) ||
            (customer.Country != null && EF.Functions.Like(customer.Country, pattern)));
    }

    public static IQueryable<Customer> OrderByCompany(this IQueryable<Customer> query) =>
        query.OrderBy(customer => customer.CompanyName ?? string.Empty)
             .ThenBy(customer => customer.CustomerId);

    public static IQueryable<CustomerSummaryDto> ProjectToSummary(this IQueryable<Customer> query) =>
        query.Select(customer => new CustomerSummaryDto(
            customer.CustomerId,
            customer.CompanyName,
            customer.ContactName,
            customer.ContactTitle,
            customer.City,
            customer.Country,
            customer.Phone,
            customer.Fax));
}
