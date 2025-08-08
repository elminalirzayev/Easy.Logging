using Easy.Logging.Core;
using System.Threading.Channels;

namespace Easy.Logging.BufferedSink
{
    public class BufferedSink : ILogSink, IDisposable
    {
        private readonly Channel<LogEntry> _channel = Channel.CreateUnbounded<LogEntry>();
        private readonly ILogSink _innerSink;
        private readonly CancellationTokenSource _cts = new();

        public BufferedSink(ILogSink innerSink)
        {
            _innerSink = innerSink;
            Task.Run(ProcessQueueAsync);
        }

        public async Task EmitAsync(LogEntry entry)
        {
            await _channel.Writer.WriteAsync(entry);
        }

        private async Task ProcessQueueAsync()
        {
            await foreach (var entry in _channel.Reader.ReadAllAsync(_cts.Token))
            {
                await _innerSink.EmitAsync(entry);
            }
        }

        public void Dispose()
        {
            _cts.Cancel();
        }
    }

}
