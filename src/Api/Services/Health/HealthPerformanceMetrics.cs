namespace Api.Services.Health;

public class HealthPerformanceMetrics : IHealthPerformanceMetrics
{
    ILogger<HealthPerformanceMetrics> _logger;
    public HealthPerformanceMetrics(ILogger<HealthPerformanceMetrics> logger)
    {
        _logger = logger;
    }

    public async Task GetApplicationHostHealthMetrics()
    {
        await Task.CompletedTask;
    }

    public void LogHealthInfo(string sourceMethodName)
    {
        long allBytes = GC.GetTotalMemory(true);
        _logger.LogWarning($"{sourceMethodName} : GC Memory : {(double)(allBytes / 1024 / 1024)} MB,  ,Process Id {Environment.ProcessPath}/{Environment.ProcessId} at {DateTime.UtcNow}");

    }
}