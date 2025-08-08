namespace Easy.Logging.Core
{
    public abstract class FilteredSinkBase : ILogSink
    {
        protected LogLevel MinLevel { get; }

        protected FilteredSinkBase(LogLevel minLevel)
        {
            MinLevel = minLevel;
        }

        public async Task EmitAsync(LogEntry entry)
        {
            if (entry.Level >= MinLevel)
                await WriteAsync(entry);
        }

        protected abstract Task WriteAsync(LogEntry entry);
    }

}
