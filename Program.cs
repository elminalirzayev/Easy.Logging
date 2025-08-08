//using Easy.Logging.Core;
//using Easy.Logging.Extensions; // Extension methods namespace
//using Microsoft.Extensions.DependencyInjection;

//class Program
//{
//    static async Task Main(string[] args)
//    {
//        // Scenario 1: Console + File logging
//        var services1 = new ServiceCollection();
//        services1.AddJsonFormatter()
//                 .AddConsoleLogger()
//                 .AddRollingFileLogger("Logs")
//                 .AddCompositeLogger();
//        var provider1 = services1.BuildServiceProvider();
//        var logger1 = provider1.GetRequiredService<ILogger>();
//        await logger1.InfoAsync("Scenario 1: Console + File log");

//        // Scenario 2: Console + Redis logging
//        var services2 = new ServiceCollection();
//        services2.AddJsonFormatter()
//                 .AddConsoleLogger()
//                 .AddRedisLogger("localhost")
//                 .AddCompositeLogger();
//        var provider2 = services2.BuildServiceProvider();
//        var logger2 = provider2.GetRequiredService<ILogger>();
//        await logger2.InfoAsync("Scenario 2: Console + Redis log");

//        // Scenario 3: Console + File + Redis logging
//        var services3 = new ServiceCollection();
//        services3.AddJsonFormatter()
//                 .AddConsoleLogger()
//                 .AddRollingFileLogger("Logs")
//                 .AddRedisLogger("localhost")
//                 .AddCompositeLogger();
//        var provider3 = services3.BuildServiceProvider();
//        var logger3 = provider3.GetRequiredService<ILogger>();
//        await logger3.InfoAsync("Scenario 3: Console + File + Redis log");

//        // Scenario 4: Console + File + Redis + Database logging
//        var services4 = new ServiceCollection();
//        // LogDbContext must be registered separately with proper options
//        services4.AddJsonFormatter()
//                 .AddConsoleLogger()
//                 .AddRollingFileLogger("Logs")
//                 .AddRedisLogger("localhost")
//                 //.AddDatabaseLogger<LogDbContext>()
//                 .AddCompositeLogger();
//        var provider4 = services4.BuildServiceProvider();
//        var logger4 = provider4.GetRequiredService<ILogger>();
//        await logger4.InfoAsync("Scenario 4: Console + File + Redis + Database log");
//    }
//}

//Console.WriteLine("Logging scenarios executed successfully. Check the logs in the specified sinks.");
