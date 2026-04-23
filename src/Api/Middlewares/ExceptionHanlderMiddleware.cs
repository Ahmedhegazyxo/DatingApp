using System.Net;
using System.Security.Authentication;
using System.Text.Json;
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

            if (!context.Response.HasStarted &&
                context.Response.StatusCode >= 400)
            {
                await HandleHttpStatusCodeAsync(context);
            }
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

        await WriteErrorResponse(context, statusCode, title, detail);
    }
    private static async Task HandleHttpStatusCodeAsync(HttpContext context)
    {
        var statusCode = (HttpStatusCode)context.Response.StatusCode;

        var title = statusCode switch
        {
            HttpStatusCode.NotFound => "Resource not found",
            HttpStatusCode.Forbidden => "Access denied",
            HttpStatusCode.Unauthorized => "Unauthorized",
            HttpStatusCode.MethodNotAllowed => "Method not allowed",
            _ => "Request failed"
        };

        await WriteErrorResponse(
            context,
            statusCode,
            title,
            $"HTTP {(int)statusCode}"
        );
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