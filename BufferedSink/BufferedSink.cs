using Easy.Logging.Core;
using System.Threading.Channels;

namespace Easy.Logging.BufferedSink
{
    public class BufferedSink : ILogSink, IDisposable
    {
        private readonly Channel<LogEntry> _channel = Channel.CreateUnbounded<LogEntry>();
        private readonly ILogSink _innerSink;
        private readonly CancellationTokenSource _cts = new();
        private readonly Task _processingTask;

        public BufferedSink(ILogSink innerSink)
        {
            _innerSink = innerSink;
            _processingTask = Task.Run(ProcessQueueAsync);
        }

        public async Task EmitAsync(LogEntry entry)
        {
            await _channel.Writer.WriteAsync(entry);
        }

#if NET6_0_OR_GREATER || NET7_0_OR_GREATER || NET8_0_OR_GREATER || NET9_0_OR_GREATER

        private async Task ProcessQueueAsync()
        {
            await foreach (var entry in _channel.Reader.ReadAllAsync(_cts.Token))
            {
                await _innerSink.EmitAsync(entry);
            }
        }
       
#endif
#if NETSTANDARD2_1_OR_GREATER || NETFRAMEWORK


        private async Task ProcessQueueAsync()
        {
            var reader = _channel.Reader;

            while (await reader.WaitToReadAsync(_cts.Token))
            {
                while (reader.TryRead(out var entry))
                {
                    try
                    {
                        await _innerSink.EmitAsync(entry);
                    }
                    catch
                    {
                        // Optional: log or ignore processing errors
                    }
                }
            }
        }
#endif
        public void Dispose()
        {
            _cts.Cancel();
            _processingTask.Wait(); // ensure queue processing finishe
        }
    }

}
