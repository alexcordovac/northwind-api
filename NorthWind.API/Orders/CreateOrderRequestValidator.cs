using FluentValidation;

namespace NorthWind.API.Orders;

public sealed class CreateOrderRequestValidator : AbstractValidator<CreateOrderRequest>
{
    public CreateOrderRequestValidator()
    {
        RuleFor(request => request.CustomerId)
            .NotEmpty()
            .MaximumLength(5);

        RuleFor(request => request.EmployeeId)
            .GreaterThan(0)
            .When(request => request.EmployeeId.HasValue);

        RuleFor(request => request.ShipVia)
            .GreaterThan(0)
            .When(request => request.ShipVia.HasValue);

        RuleFor(request => request.Freight)
            .GreaterThanOrEqualTo(0m)
            .When(request => request.Freight.HasValue);

        RuleFor(request => request.ShipName)
            .MaximumLength(40)
            .When(request => !string.IsNullOrWhiteSpace(request.ShipName));

        RuleFor(request => request.ShipAddress)
            .MaximumLength(60)
            .When(request => !string.IsNullOrWhiteSpace(request.ShipAddress));

        RuleFor(request => request.ShipCity)
            .MaximumLength(15)
            .When(request => !string.IsNullOrWhiteSpace(request.ShipCity));

        RuleFor(request => request.ShipRegion)
            .MaximumLength(15)
            .When(request => !string.IsNullOrWhiteSpace(request.ShipRegion));

        RuleFor(request => request.ShipPostalCode)
            .MaximumLength(10)
            .When(request => !string.IsNullOrWhiteSpace(request.ShipPostalCode));

        RuleFor(request => request.ShipCountry)
            .MaximumLength(15)
            .When(request => !string.IsNullOrWhiteSpace(request.ShipCountry));

        RuleFor(request => request.Details)
            .NotNull()
            .WithMessage("Order details are required.")
            .NotEmpty()
            .WithMessage("Order must contain at least one detail.");

        RuleForEach(request => request.Details)
            .SetValidator(new CreateOrderDetailRequestValidator())
            .When(request => request.Details is not null);
    }
}

public sealed class CreateOrderDetailRequestValidator : AbstractValidator<CreateOrderDetailRequest>
{
    public CreateOrderDetailRequestValidator()
    {
        RuleFor(detail => detail.ProductId)
            .GreaterThan(0);

        RuleFor(detail => detail.UnitPrice)
            .GreaterThanOrEqualTo(0m);

        RuleFor(detail => detail.Quantity)
            .GreaterThan((short)0);

        RuleFor(detail => detail.Discount)
            .InclusiveBetween(0f, 1f);
    }
}
