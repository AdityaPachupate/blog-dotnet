using System.Net;
using System.Text.Json;
using Blog.Domain.Exceptions;

namespace Blog.API.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var (statusCode, message) = exception switch
            {
                NotFoundException => (HttpStatusCode.NotFound, exception.Message),
                UnauthorizedException => (HttpStatusCode.Unauthorized, exception.Message),
                ForbidException => (HttpStatusCode.Forbidden, "You do not have permission to perform this action"),
                _ => (HttpStatusCode.InternalServerError, "An error occurred while processing your request")
            };

            context.Response.StatusCode = (int)statusCode;

            var response = new
            {
                error = new
                {
                    message,
                    statusCode = (int)statusCode
                }
            };

            var jsonResponse = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(jsonResponse);
        }
    }
} 