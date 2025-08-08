using Easy.Logging.Core;

namespace Easy.Logging.Console_
{
    /// <summary>
    /// Logs messages to the console using the provided formatter.
    /// </summary>
    public class ConsoleSink : ILogSink
    {
        private readonly ILogFormatter _formatter;

        public ConsoleSink(ILogFormatter formatter)
        {
            _formatter = formatter;
        }

        public Task EmitAsync(LogEntry entry)
        {
            var formattedMessage = _formatter.Format(entry);
            Console.WriteLine(formattedMessage);
            return Task.CompletedTask;
        }
    }
}
