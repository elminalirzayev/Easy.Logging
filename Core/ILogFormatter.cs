namespace Easy.Logging.Core
{
    /// <summary>
    /// Formatter interface to format LogEntry objects into strings.
    /// </summary>
    public interface ILogFormatter
    {
        string Format(LogEntry entry);
    }
}
