using Easy.Logging.Core;
using System.Text.Json;

namespace Easy.Logging.JsonFormatter
{
    /// <summary>
    /// Formats LogEntry objects as JSON strings.
    /// </summary>
    public class JsonLogFormatter : ILogFormatter
    {
        private readonly JsonSerializerOptions _options;

        public JsonLogFormatter(JsonSerializerOptions? options = null)
        {
            _options = options ?? new JsonSerializerOptions
            {
                WriteIndented = false,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
        }

        public string Format(LogEntry entry)
        {
            return JsonSerializer.Serialize(entry, _options);
        }
    }
}
