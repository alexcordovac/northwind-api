using FluentValidation;
using NorthWind.API.Orders;

namespace NorthWind.Application.Orders.Validators;

public sealed class GetOrdersRequestValidator : AbstractValidator<GetOrdersRequest>
{
    public GetOrdersRequestValidator()
    {
        RuleFor(input => input.Page)
            .GreaterThanOrEqualTo(1);

        RuleFor(input => input.Rows)
            .GreaterThan(0);

        RuleFor(input => input.Offset)
            .GreaterThanOrEqualTo(0);

        RuleFor(input => input.Query)
            .MaximumLength(200)
            .When(input => !string.IsNullOrWhiteSpace(input.Query));
    }
}
