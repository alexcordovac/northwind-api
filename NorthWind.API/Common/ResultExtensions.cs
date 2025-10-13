using FluentResults;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NorthWind.Application.Common.Errors;
using System;
using System.Collections.Generic;
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

        ProblemDetails problem = CreateProblemDetails(result.Errors);

        return TypedResults.Problem(problem);
    }

    internal static IResult ToHttpResult<TValue>(this Result<TValue> result, Func<TValue, IResult> successFactory)
    {
        if (result.IsSuccess)
        {
            return successFactory(result.Value);
        }

        ProblemDetails problem = CreateProblemDetails(result.Errors);

        return TypedResults.Problem(problem);
    }

    internal static IResult ToHttpResult(this Result result, Func<IResult>? successFactory = null)
    {
        if (result.IsSuccess)
        {
            return successFactory?.Invoke() ?? TypedResults.NoContent();
        }

        ProblemDetails problem = CreateProblemDetails(result.Errors);

        return TypedResults.Problem(problem);
    }

    private static ProblemDetails CreateProblemDetails(IReadOnlyCollection<IError> errors)
    {
        IError? firstError = errors.FirstOrDefault();

        return new ProblemDetails
        {
            Title = "Request cannot be processed.",
            Detail = string.Join("; ", errors.Select(error => error.Message)),
            Status = firstError?.ToHttpStatus() ?? StatusCodes.Status400BadRequest,
        };
    }

    internal static int ToHttpStatus(this IError error) => error switch
    {
        ForbidError => StatusCodes.Status403Forbidden,
        NotFoundError => StatusCodes.Status404NotFound,
        BadRequestError => StatusCodes.Status400BadRequest,
        _ => StatusCodes.Status400BadRequest
    };
}
