using Easy.Logging.Composite;
using Easy.Logging.Console_;
using Easy.Logging.Core;
using Easy.Logging.DatabaseSink_;
using Easy.Logging.File_;
using Easy.Logging.JsonFormatter;
using Easy.Logging.RedisSink_;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

#if NET6_0_OR_GREATER || NET7_0_OR_GREATER || NET8_0_OR_GREATER
using Microsoft.EntityFrameworkCore;
#endif
#if NETSTANDARD2_1_OR_GREATER || NETFRAMEWORK
using System.Data.Entity;
#endif


namespace Easy.Logging.Extensions
{
    public static class LoggingServiceCollectionExtensions
    {
        /// <summary>
        /// Adds JSON formatter as singleton.
        /// </summary>
        public static IServiceCollection AddJsonFormatter(this IServiceCollection services)
        {
            services.AddSingleton<ILogFormatter, JsonLogFormatter>();
            return services;
        }

        /// <summary>
        /// Adds Console sink logger.
        /// </summary>
        public static IServiceCollection AddConsoleLogger(this IServiceCollection services)
        {
            services.AddSingleton<ILogSink, ConsoleSink>();
            return services;
        }

        /// <summary>
        /// Adds rolling file logger sink.
        /// </summary>
        public static IServiceCollection AddRollingFileLogger(this IServiceCollection services, string logDirectory = "Logs")
        {
            services.AddSingleton<ILogSink>(provider =>
            {
                var formatter = provider.GetRequiredService<ILogFormatter>();
                return new RollingFileLogger(logDirectory, formatter);
            });
            return services;
        }

        /// <summary>
        /// Adds EF Core based database logger sink.
        /// </summary>
        public static IServiceCollection AddDatabaseLogger<TContext>(this IServiceCollection services)
            where TContext : DbContext
        {
            services.AddScoped<ILogSink, DatabaseSink>();
            return services;
        }

        /// <summary>
        /// Adds Redis logger sink.
        /// </summary>
        public static IServiceCollection AddRedisLogger(this IServiceCollection services, string redisConnectionString = "localhost", string listKey = "logs")
        {
            services.AddSingleton<IConnectionMultiplexer>(sp => ConnectionMultiplexer.Connect(redisConnectionString));
            services.AddSingleton<ILogSink>(sp =>
            {
                var redis = sp.GetRequiredService<IConnectionMultiplexer>();
                return new RedisSink(redis, listKey);
            });
            return services;
        }

        /// <summary>
        /// Adds composite logger that sends logs to all registered sinks.
        /// </summary>
        public static IServiceCollection AddCompositeLogger(this IServiceCollection services)
        {
            services.AddSingleton<ILogger>(sp =>
            {
                var sinks = sp.GetServices<ILogSink>();
                return new CompositeLogger(sinks);
            });
            return services;
        }
    }
}
