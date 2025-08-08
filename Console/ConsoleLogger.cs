using Easy.Logging.Core;

namespace Easy.Logging.Console_
{
    /// <summary>
    /// Logger implementation that writes log messages to the console.
    /// </summary>
    public class ConsoleLogger : ILogger
    {
        private readonly ILogFormatter _formatter;

        public ConsoleLogger(ILogFormatter formatter)
        {
            _formatter = formatter;
        }

        public Task LogAsync(LogEntry entry)
        {
            var formatted = _formatter.Format(entry);
            Console.WriteLine(formatted);
            return Task.CompletedTask;
        }

        public Task TraceAsync(string message, string? context = null)
            => LogAsync(new LogEntry { Level = LogLevel.Trace, Message = message, Timestamp = DateTime.UtcNow, Context = context });

        public Task DebugAsync(string message, string? context = null)
            => LogAsync(new LogEntry { Level = LogLevel.Debug, Message = message, Timestamp = DateTime.UtcNow, Context = context });

        public Task InfoAsync(string message, string? context = null)
            => LogAsync(new LogEntry { Level = LogLevel.Info, Message = message, Timestamp = DateTime.UtcNow, Context = context });

        public Task WarnAsync(string message, string? context = null)
            => LogAsync(new LogEntry { Level = LogLevel.Warn, Message = message, Timestamp = DateTime.UtcNow, Context = context });

        public Task ErrorAsync(string message, string? context = null, string? exception = null)
            => LogAsync(new LogEntry { Level = LogLevel.Error, Message = message, Timestamp = DateTime.UtcNow, Context = context, Exception = exception });

        public Task FatalAsync(string message, string? context = null, string? exception = null)
            => LogAsync(new LogEntry { Level = LogLevel.Fatal, Message = message, Timestamp = DateTime.UtcNow, Context = context, Exception = exception });
    }
}
