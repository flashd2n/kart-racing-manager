using KarRacingManager.Models.SqliteModels;
using KartRacingManager.Data.SqliteMigrations;
using KartRacingManager.Interfaces.Data;
using System.Data.Entity;

namespace KartRacingManager.Data
{
    public class LogsDbContext : DbContext, ILogsDbContext, IDbContext
    {
        public LogsDbContext() : base("SqliteDb")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<LogsDbContext, LogsDbConfig>(true));
        }

        public IDbSet<Log> Logs { get; set; }

        public IDbSet<LogType> LogTypes { get; set; }

        public new IDbSet<TEntity> Set<TEntity>()
            where TEntity : class
        {
            return base.Set<TEntity>();
        }
    }
}
