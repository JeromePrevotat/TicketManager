using System;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;

namespace TicketManagerApi.Validators;

public class ValidationExceptionHandler : IExceptionHandler
{
  public async ValueTask<bool> TryHandleAsync
  (
    HttpContext httpContext,
    Exception exception,
    CancellationToken cancellation
  )
  {
    if (exception is not ValidationException validationException)
    {
      return false;
    }

    var errors = validationException.Errors
      .GroupBy(e => e.PropertyName)
      .ToDictionary(
        g => g.Key,
        g => g.Select(e => e.ErrorMessage).ToArray()
      );

    httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
    httpContext.Response.ContentType = "application/json";

    await httpContext.Response.WriteAsJsonAsync
    (
      new HttpValidationProblemDetails(errors)
      {
        Status = StatusCodes.Status400BadRequest,
        Title = "Validation failed",
      }, cancellation
    );
    return true;
  }
}
