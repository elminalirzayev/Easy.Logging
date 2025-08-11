using Easy.Logging.Core;
using System.Collections.Generic;


#if NET6_0_OR_GREATER || NET7_0_OR_GREATER || NET8_0_OR_GREATER || NET9_0_OR_GREATER
using Microsoft.EntityFrameworkCore;
#endif
#if NETSTANDARD2_1_OR_GREATER ||NETFRAMEWORK
using System.Data.Entity;
#endif

namespace Easy.Logging.DatabaseSink_
{

#if NET6_0_OR_GREATER || NET7_0_OR_GREATER || NET8_0_OR_GREATER || NET9_0_OR_GREATER
    public class LogDbContext : DbContext
    {
        public DbSet<LogEntryEntity> Logs { get; set; }

        public LogDbContext(DbContextOptions<LogDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<LogEntryEntity>().ToTable("Logs");
        }
    }
#endif
#if NETSTANDARD2_1_OR_GREATER ||NETFRAMEWORK
    public class LogDbContext : DbContext
    {
        public DbSet<LogEntryEntity>? Logs { get; set; }

        public LogDbContext() : base("name=LogDbConnection") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<LogEntryEntity>().ToTable("Logs");
        }
    }
#endif

    public class LogEntryEntity
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string Level { get; set; } = null!;
        public string Message { get; set; } = null!;
        public string? Context { get; set; }
        public string? Exception { get; set; }
    }

    public class DatabaseSink : ILogSink
    {
        private readonly LogDbContext _context;

        public DatabaseSink(LogDbContext context)
        {
            _context = context;
        }

        public async Task EmitAsync(LogEntry entry)
        {
            var entity = new LogEntryEntity
            {
                Timestamp = entry.Timestamp,
                Level = entry.Level.ToString(),
                Message = entry.Message,
                Context = entry.Context,
                Exception = entry.Exception
            };

            _context.Logs?.Add(entity);
#if NET6_0_OR_GREATER || NET7_0_OR_GREATER || NET8_0_OR_GREATER || NET9_0_OR_GREATER
            await _context.SaveChangesAsync();
#endif
#if NETSTANDARD2_1_OR_GREATER || NETFRAMEWORK
            await Task.Run(() => _context.SaveChanges());
#endif
        }
    }
}
