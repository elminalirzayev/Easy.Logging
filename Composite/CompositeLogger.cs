using Easy.Logging.Core;

namespace Easy.Logging.Composite
{
    /// <summary>
    /// Logger that dispatches log entries to multiple sinks concurrently.
    /// </summary>
    public class CompositeLogger : ILogger
    {
        private readonly IEnumerable<ILogSink> _sinks;

        public CompositeLogger(IEnumerable<ILogSink> sinks)
        {
            _sinks = sinks;
        }

        public async Task LogAsync(LogEntry entry)
        {
            var tasks = _sinks.Select(sink => sink.EmitAsync(entry));
            await Task.WhenAll(tasks);
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
