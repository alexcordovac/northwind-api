using Microsoft.EntityFrameworkCore;
using NorthWind.Application.Orders.DTOs;
using NorthWind.Domain.Entities;

namespace NorthWind.Application.Orders.Queries;

public static class OrderQueries
{
    public static IQueryable<Order> WithoutTracking(this IQueryable<Order> query) =>
        query.AsNoTracking();

    public static IQueryable<Order> ApplySearch(this IQueryable<Order> query, string? searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            return query;
        }

        string pattern = $"%{searchTerm.Trim()}%";

        return query.Where(order =>
            (order.Customer != null && order.Customer.CompanyName != null && EF.Functions.Like(order.Customer.CompanyName, pattern)) ||
            (order.Customer != null && order.Customer.ContactName != null && EF.Functions.Like(order.Customer.ContactName, pattern)) ||
            (order.ShipName != null && EF.Functions.Like(order.ShipName, pattern)) ||
            (order.ShipCity != null && EF.Functions.Like(order.ShipCity, pattern)) ||
            (order.ShipCountry != null && EF.Functions.Like(order.ShipCountry, pattern)));
    }

    public static IQueryable<Order> OrderByMostRecent(this IQueryable<Order> query) =>
        query.OrderByDescending(order => order.OrderDate ?? DateTime.MinValue)
             .ThenByDescending(order => order.OrderId);

    public static IQueryable<T> ApplyPagination<T>(this IQueryable<T> query, int skip, int take)
    {
        if (skip > 0)
        {
            query = query.Skip(skip);
        }

        if (take > 0)
        {
            query = query.Take(take);
        }

        return query;
    }

    public static IQueryable<OrderSummaryDto> ProjectToSummary(this IQueryable<Order> query) =>
        query.Select(order => new OrderSummaryDto(
            order.OrderId,
            order.CustomerId,
            order.Customer != null ? order.Customer.CompanyName : null,
            order.OrderDate,
            order.RequiredDate,
            order.ShippedDate,
            order.Freight,
            order.ShipName,
            order.ShipCity,
            order.ShipCountry));
}
