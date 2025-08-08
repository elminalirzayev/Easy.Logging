namespace Easy.Logging.Core
{
    /// <summary>
    /// Represents a single log entry.
    /// </summary>
    public class LogEntry
    {
        public DateTime Timestamp { get; set; }
        public LogLevel Level { get; set; }
        public string Message { get; set; } = string.Empty;
        public string? Exception { get; set; }
        public string? LoggerName { get; set; }
        public string? Context { get; set; } // Optional contextual info (e.g., request id)
    }
}
