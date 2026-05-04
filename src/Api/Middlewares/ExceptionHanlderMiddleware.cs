using System.Net;
using System.Security.Authentication;
using System.Text.Json;
using Castle.DynamicProxy;
using Microsoft.IdentityModel.Tokens;

namespace Api.Middlewares;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;

    public ExceptionHandlerMiddleware(
        RequestDelegate next,
        ILogger<ExceptionHandlerMiddleware> logger)
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
            await HandleExceptionAsync(context, ex);
        }
    }
    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        _logger.LogError(ex, ex.Message);

        var (statusCode, title, detail) = ex switch
        {
            InvalidCredentialException =>
                (HttpStatusCode.Unauthorized, "Invalid credentials", ex.Message),

            AuthenticationException =>
                (HttpStatusCode.Unauthorized, "Authentication failed", ex.Message),

            SecurityTokenExpiredException =>
                (HttpStatusCode.Unauthorized, "Token expired", "JWT token has expired"),

            SecurityTokenException =>
                (HttpStatusCode.Unauthorized, "Invalid token", ex.Message),

            ArgumentException =>
                (HttpStatusCode.BadRequest, "Bad request", ex.Message),

            _ =>
                (HttpStatusCode.InternalServerError,
                 "Internal server error",
                 "An unexpected error occurred")
        };

        try
        {
            await WriteErrorResponse(context, statusCode, title, detail);
        }
        catch (Exception writeEx)
        {
            _logger.LogError(writeEx, "Failed to write error response");
        }
    }
    private static async Task WriteErrorResponse(
        HttpContext context,
        HttpStatusCode statusCode,
        string title,
        string detail)
    {
        if (context.Response.HasStarted)
            return;

        context.Response.Clear();
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var response = new
        {
            status = context.Response.StatusCode,
            title,
            detail
        };

        await context.Response.WriteAsync(
            JsonSerializer.Serialize(response)
        );
    }
}