using Easy.Logging.Core;

namespace Easy.Logging.File_
{
    /// <summary>
    /// RollingFileLogger writes log entries asynchronously into daily rolling files.
    /// Implements ILogSink for compatibility with logging pipeline.
    /// </summary>
    public class RollingFileLogger : ILogSink, IDisposable
    {
        private readonly string _logDirectory;
        private readonly ILogFormatter _formatter;
        private readonly object _lock = new object();
        private StreamWriter _currentWriter;
        private DateTime _currentDate;

        public RollingFileLogger(string logDirectory, ILogFormatter formatter)
        {
            _logDirectory = logDirectory ?? throw new ArgumentNullException(nameof(logDirectory));
            _formatter = formatter ?? throw new ArgumentNullException(nameof(formatter));
            Directory.CreateDirectory(_logDirectory);
            _currentDate = DateTime.UtcNow.Date;
            _currentWriter = CreateStreamWriter(_currentDate);
        }

        /// <summary>
        /// Emits a log entry to the current day's log file asynchronously.
        /// Rolls file daily based on UTC date.
        /// </summary>
        public Task EmitAsync(LogEntry entry)
        {
            var logDate = DateTime.UtcNow.Date;

            lock (_lock)
            {
                if (logDate != _currentDate)
                {
                    _currentWriter.Dispose();
                    _currentDate = logDate;
                    _currentWriter = CreateStreamWriter(_currentDate);
                }

                var formatted = _formatter.Format(entry);
                _currentWriter.WriteLine(formatted);
                _currentWriter.Flush();
            }

            return Task.CompletedTask;
        }

        private StreamWriter CreateStreamWriter(DateTime date)
        {
            var filePath = Path.Combine(_logDirectory, $"log-{date:yyyy-MM-dd}.txt");
            var fileStream = new FileStream(filePath, FileMode.Append, FileAccess.Write, FileShare.Read);
            return new StreamWriter(fileStream) { AutoFlush = true };
        }

        public void Dispose()
        {
            _currentWriter?.Dispose();
        }
    }
}
