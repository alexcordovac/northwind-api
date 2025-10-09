using FluentResults;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NorthWind.Application.Common.Errors;
using System.Linq;

namespace NorthWind.API.Common;

internal static class ResultExtensions
{
    internal static IResult ToHttpResult<TValue>(this Result<TValue> result)
    {
        if (result.IsSuccess)
        {
            return TypedResults.Ok(result.Value);
        }

        ProblemDetails problem = new()
        {
            Title = "Request cannot be processed.",
            Detail = string.Join("; ", result.Errors.Select(error => error.Message)),
            Status = result.Errors.FirstOrDefault().ToHttpStatus(),
        };

        return TypedResults.Problem(problem);
    }

    internal static int ToHttpStatus(this IError error) => error switch
    {
        ForbidError => StatusCodes.Status403Forbidden,
        NotFoundError => StatusCodes.Status404NotFound,
        BadRequestError => StatusCodes.Status400BadRequest,
        _ => StatusCodes.Status400BadRequest
    };
}
