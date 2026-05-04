namespace Api.Services.Health;
public interface IHealthPerformanceMetrics
{
    public void LogHealthInfo(string sourceMethodName);
    public Task GetApplicationHostHealthMetrics();
}