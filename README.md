[![Build & Test](https://github.com/elminalirzayev/Easy.Logging/actions/workflows/build.yml/badge.svg)](https://github.com/elminalirzayev/Easy.Logging/actions/workflows/build.yml)
[![Build & Release](https://github.com/elminalirzayev/Easy.Logging/actions/workflows/release.yml/badge.svg)](https://github.com/elminalirzayev/Easy.Logging/actions/workflows/release.yml)
[![Build & Nuget Publish](https://github.com/elminalirzayev/Easy.Logging/actions/workflows/nuget.yml/badge.svg)](https://github.com/elminalirzayev/Easy.Logging/actions/workflows/nuget.yml)
[![Release](https://img.shields.io/github/v/release/elminalirzayev/Easy.Logging)](https://github.com/elminalirzayev/Easy.Logging/releases)
[![License](https://img.shields.io/github/license/elminalirzayev/Easy.Logging)](https://github.com/elminalirzayev/Easy.Logging/blob/master/LICENSE.txt)

# Easy.Logging

Easy.Logging is a lightweight, high-performance logging library for .NET, supporting multiple sinks (Console, File, Redis, Database, Buffered, Composite) and pluggable serialization (e.g., JSON). It is designed for flexibility, extensibility, and easy integration with dependency injection.

## Features
- Console, File, Rolling File, Redis, and Database logging sinks
- Buffered and Composite logging
- Pluggable log formatters (e.g., JSON)
- Asynchronous logging
- .NET Standard 2.1 support
- Designed for DI (Microsoft.Extensions.DependencyInjection)

## Installation

Install via NuGet:
Install-Package Easy.Logging
## Quick Start
using Easy.Logging.Extensions; // Extension methods namespace
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddJsonFormatter()
        .AddConsoleLogger()
        .AddRollingFileLogger("Logs")
        .AddRedisLogger("localhost")
        .AddCompositeLogger();
var provider = services.BuildServiceProvider();
var logger = provider.GetRequiredService<ILogger>();
await logger.InfoAsync("Hello from Easy.Logging!");

## Sinks
- **ConsoleSink**: Logs to the console
- **FileLogger**: Logs to a single file
- **RollingFileLogger**: Logs to daily rolling files
- **RedisSink**: Logs to Redis lists
- **DatabaseSink**: Logs to a database (Entity Framework/EF Core)
- **BufferedSink**: Buffers logs and writes asynchronously
- **CompositeLogger**: Dispatches logs to multiple sinks

## Extending
You can implement your own `ILogSink` or `ILogFormatter` for custom sinks or formats.

## License
MIT