using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace NorthWind.API.Middlewares
{
    public class ValidationFilter<TModel> : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var validator = context.HttpContext.RequestServices.GetService<IValidator<TModel>>();

            if (validator is not null)
            {
                foreach (var argument in context.Arguments)
                {
                    if (argument is TModel model)
                    {
                        var validationResult = await validator.ValidateAsync(model);
                        if (!validationResult.IsValid)
                        {
                            var problemDetails = new ValidationProblemDetails(validationResult.ToDictionary());
                            return Results.ValidationProblem(problemDetails.Errors);
                        }
                    }
                }
            }

            return await next(context);
        }
    }
}
