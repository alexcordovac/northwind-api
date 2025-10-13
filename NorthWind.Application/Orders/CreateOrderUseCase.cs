using FluentResults;
using Microsoft.EntityFrameworkCore;
using NorthWind.Application.Common;
using NorthWind.Application.Common.Errors;
using NorthWind.Application.Orders.DTOs;
using NorthWind.Application.Orders.Queries;
using NorthWind.Domain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NorthWind.Application.Orders;

public sealed class CreateOrderUseCase
{
    private readonly INorthWindDbContext _dbContext;

    public CreateOrderUseCase(INorthWindDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<OrderSummaryDto>> ExecuteAsync(CreateOrderInput input, CancellationToken cancellationToken)
    {
        if (input.Details is null || input.Details.Count == 0)
        {
            return Result.Fail<OrderSummaryDto>(new BadRequestError("Order must contain at least one detail."));
        }

        Order order = Order.Create(
            input.CustomerId,
            input.EmployeeId,
            input.OrderDate,
            input.RequiredDate,
            input.ShippedDate,
            input.ShipVia,
            input.Freight,
            input.ShipName,
            input.ShipAddress,
            input.ShipCity,
            input.ShipRegion,
            input.ShipPostalCode,
            input.ShipCountry);

        foreach (CreateOrderDetailInput detail in input.Details)
        {
            order.AddDetail(
                detail.ProductId,
                detail.UnitPrice,
                detail.Quantity,
                detail.Discount);
        }

        await _dbContext.Orders.AddAsync(order, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        OrderSummaryDto summary = await _dbContext
            .Orders
            .Where(createdOrder => createdOrder.OrderId == order.OrderId)
            .ProjectToSummary()
            .SingleAsync(cancellationToken);

        return Result.Ok(summary);
    }
}
