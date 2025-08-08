namespace Easy.Logging.Core
{
    public interface ILogSink
    {
        Task EmitAsync(LogEntry entry);
    }

}
