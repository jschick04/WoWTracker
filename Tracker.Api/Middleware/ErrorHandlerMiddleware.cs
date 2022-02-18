using System.Net;
using System.Text.Json;
using Tracker.Api.Library.Helpers;

namespace Tracker.Api.Middleware;

public class ErrorHandlerMiddleware {

    private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware(RequestDelegate next) => _next = next;

    public async Task Invoke(HttpContext context) {
        try {
            await _next(context);
        } catch (Exception exception) {
            var response = context.Response;
            response.ContentType = "application/json";

            response.StatusCode = exception switch {
                ApiException => (int)HttpStatusCode.BadRequest,
                KeyNotFoundException => (int)HttpStatusCode.NotFound,
                _ => (int)HttpStatusCode.InternalServerError,
            };

            var result = JsonSerializer.Serialize(new { Error = exception?.Message });
            await response.WriteAsync(result);
        }
    }

}