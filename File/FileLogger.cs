using Easy.Logging.Core;

namespace Easy.Logging.File_
{
    /// <summary>
    /// Logger implementation that writes log messages to a file asynchronously.
    /// </summary>
    public class FileLogger : ILogger, IDisposable
    {
        private readonly ILogFormatter _formatter;
        private readonly string _filePath;
        private readonly object _lockObj = new();

        public FileLogger(string filePath, ILogFormatter formatter)
        {
            _filePath = filePath;
            _formatter = formatter;
        }

        public async Task LogAsync(LogEntry entry)
        {
            var formatted = _formatter.Format(entry);
            // Ensures thread-safe writes
            lock (_lockObj)
            {
                File.AppendAllText(_filePath, formatted + Environment.NewLine);
            }
            await Task.CompletedTask;
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

        public void Dispose()
        {
            // No unmanaged resources, nothing to dispose now
        }
    }
}
