using KarRacingManager.Models.SqliteModels;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace KartRacingManager.Interfaces.Data
{
    public interface ILogsDbContext
    {
        IDbSet<Log> Logs { get; set; }

        IDbSet<LogType> LogTypes { get; set; }

        IDbSet<TEntity> Set<TEntity>() where TEntity : class;

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        int SaveChanges();
    }
}
