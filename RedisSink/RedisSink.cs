using Easy.Logging.Core;
using StackExchange.Redis;
using System.Text.Json;

namespace Easy.Logging.RedisSink_
{
    public class RedisSink : ILogSink
    {
        private readonly IDatabase _database;
        private readonly string _listKey;

        public RedisSink(IConnectionMultiplexer redis, string listKey = "logs")
        {
            _database = redis.GetDatabase();
            _listKey = listKey;
        }

        public Task EmitAsync(LogEntry entry)
        {
            var json = JsonSerializer.Serialize(entry);
            return _database.ListLeftPushAsync(_listKey, json);
        }
    }
}
