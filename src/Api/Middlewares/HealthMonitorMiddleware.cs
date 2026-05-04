using Api.Services.Health;

namespace Api.Middlewares;

public class HealthMonitorMiddleware
{
    private readonly RequestDelegate _next;
    private IHealthPerformanceMetrics _healthPerformanceMetrics;
    public HealthMonitorMiddleware(RequestDelegate next, IHealthPerformanceMetrics healthPerformanceMetrics)
    {
        _next = next;
        _healthPerformanceMetrics = healthPerformanceMetrics;
    }
    public async Task InvokeAsync(HttpContext context, IHealthPerformanceMetrics healthPerformanceMetrics)
    {
        
        if (context.Response.HasStarted)
        {
            _healthPerformanceMetrics.LogHealthInfo($"AFTER {context.Request.Path} :");
        }
        else
        {
            _healthPerformanceMetrics.LogHealthInfo($"BEFORE {context.Request.Path} :");
        }

        await _next(context);
    }
}