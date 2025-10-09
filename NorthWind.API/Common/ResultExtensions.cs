using FluentResults;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
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
            Status = StatusCodes.Status400BadRequest
        };

        return TypedResults.BadRequest(problem);
    }
}
