using FluentResults;
using Microsoft.EntityFrameworkCore;
using NorthWind.Application.Common;
using NorthWind.Application.Common.Errors;
using NorthWind.Domain.Entities;

namespace NorthWind.Application.Orders;

public sealed class DeleteOrderUseCase
{
    private readonly INorthWindDbContext _dbContext;

    public DeleteOrderUseCase(INorthWindDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result> ExecuteAsync(int orderId, CancellationToken cancellationToken)
    {
        Order? order = await _dbContext
            .Orders
            .FirstOrDefaultAsync(order => order.OrderId == orderId, cancellationToken);

        if (order is null)
        {
            return Result.Fail(new NotFoundError($"Order with id '{orderId}' was not found."));
        }

        _dbContext.Orders.Remove(order);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}
