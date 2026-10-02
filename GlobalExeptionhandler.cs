
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EmployeeManagement.Exceptions;

namespace EmployeeManagement.GlobalExceptionHandler;

public sealed class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(
        ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var (statusCode, title, logLevel) = exception switch
        {
            EmployeeNotFoundException =>
                (StatusCodes.Status404NotFound,
                 "Employee not found",
                 LogLevel.Warning),

            _ =>
                (StatusCodes.Status500InternalServerError,
                 "An unexpected error occurred",
                 LogLevel.Error)
        };

        _logger.Log(
            logLevel,
            exception,
            "Request failed. TraceId: {TraceId}",
            httpContext.TraceIdentifier);

        httpContext.Response.StatusCode = statusCode;

        var problem = new ProblemDetails
        {
            Status = statusCode,
            Title = title
        };

        problem.Extensions["traceId"] =
            httpContext.TraceIdentifier;

        await httpContext.Response.WriteAsJsonAsync(
            problem,
            cancellationToken);

        return true;
    }
}