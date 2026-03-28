using System.Text.Json;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace TicketManagerApi.Validators;

public class ValidationExceptionHandler (
  IOptions<JsonOptions> jsonOptions
): IExceptionHandler
{
  private readonly JsonSerializerOptions _serializerOptions = jsonOptions.Value.JsonSerializerOptions;
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
      },
      _serializerOptions,
      cancellation
    );
    return true;
  }
}
