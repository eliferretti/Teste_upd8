using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace upd8.API.Middlewares
{
    public class ErrorHandlerMiddleware : IMiddleware
    {
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(ILogger<ErrorHandlerMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";

                ProblemDetails details = new ProblemDetails
                {
                    Type = "Error",
                    Title = "Internal Error",
                    Status = StatusCodes.Status500InternalServerError,
                    Detail = "An error was ocurred!"
                };

                var json = JsonSerializer.Serialize(details);

                await context.Response.WriteAsync(json);
            }
        }
    }
}
