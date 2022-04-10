using System.Net;
using System.Text.Json;

namespace Integrations.Host;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, ILogger<ErrorHandlingMiddleware> logger)
    {
        try
        {
            await _next(context);
        }
        catch (Exception error)
        {
            logger.LogError(error, "Error occured. {0}.", error.Message);

            var response = context.Response;
            response.StatusCode = (int)HttpStatusCode.FailedDependency;
            response.ContentType = "application/json";

            var result = JsonSerializer.Serialize(new { message = error?.Message });
            await response.WriteAsync(result);
        }
    }
}
