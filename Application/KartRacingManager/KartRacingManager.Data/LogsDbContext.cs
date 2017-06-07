using KarRacingManager.Models.SqliteModels;
using KartRacingManager.Data.SqliteMigrations;
using System.Data.Entity;

namespace KartRacingManager.Data
{
    public class LogsDbContext : DbContext
    {
        public LogsDbContext() : base("SqliteDb")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<LogsDbContext, LogsDbConfig>(true));
        }

        public DbSet<Log> Logs { get; set; }
        public DbSet<LogType> LogTypes { get; set; }
    }
}
