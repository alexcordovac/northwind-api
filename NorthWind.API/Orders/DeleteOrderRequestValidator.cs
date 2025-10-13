using FluentValidation;

namespace NorthWind.API.Orders;

public sealed class DeleteOrderRequestValidator : AbstractValidator<DeleteOrderRequest>
{
    public DeleteOrderRequestValidator()
    {
        RuleFor(request => request.OrderId)
            .GreaterThan(0);
    }
}
