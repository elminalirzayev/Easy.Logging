namespace Easy.Logging.Core
{
    /// <summary>
    /// Logger interface for logging messages asynchronously.
    /// </summary>
    public interface ILogger
    {
        Task LogAsync(LogEntry entry);

        // Convenience methods for different log levels
        Task TraceAsync(string message, string? context = null);
        Task DebugAsync(string message, string? context = null);
        Task InfoAsync(string message, string? context = null);
        Task WarnAsync(string message, string? context = null);
        Task ErrorAsync(string message, string? context = null, string? exception = null);
        Task FatalAsync(string message, string? context = null, string? exception = null);
    }
}
