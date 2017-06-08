using KarRacingManager.Models.SqliteModels;
using System.Data.Entity;

namespace KartRacingManager.Data.Interfaces
{
    public interface ILogsDbContext : IDbContext
    {
        IDbSet<Log> Logs { get; set; }

        IDbSet<LogType> LogTypes { get; set; }
    }
}
