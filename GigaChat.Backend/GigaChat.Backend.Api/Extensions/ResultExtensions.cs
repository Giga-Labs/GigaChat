using GigaChat.Backend.Domain.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace GigaChat.Backend.Api.Extensions;

public static class ResultExtensions
{
    public static ObjectResult ToProblem(this Result result, int statusCode)
    {
        if (result.Succeeded)
            throw new InvalidOperationException("Cannot convert success result to a problem");

        var problem = Results.Problem(statusCode: statusCode);
        var problemDetails = problem.GetType().GetProperty(nameof(ProblemDetails))!.GetValue(problem) as ProblemDetails;

        problemDetails!.Extensions = new Dictionary<string, object?>
        {
            {
                "errors", new[] { result.Error }
            }
        };

        return new ObjectResult(problemDetails);
    }
}