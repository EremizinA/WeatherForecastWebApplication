using System;

namespace WeatherForecast.Api.Logging;

public class LoggerAdapter<TType> : ILoggerAdapter<TType>
{
    private readonly ILogger<TType> _logger;

    private static string DateTimeUtcNow => $"[{DateTime.UtcNow:yyyy/MM/dd HH:mm:ss}] ";

    public LoggerAdapter(ILogger<TType> logger)
    {
        _logger = logger;
    }

    public void LogInformation(string? message, params object?[] args)
    {
        _logger.LogInformation(DateTimeUtcNow + message, args);
    }

    public void LogError(Exception exception, string message, params object?[] args)
    {
        _logger.LogError(exception, DateTimeUtcNow + message, args);
    }
}
