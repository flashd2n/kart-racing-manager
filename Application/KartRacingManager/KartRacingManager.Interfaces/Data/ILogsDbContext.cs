using KarRacingManager.Models.SqliteModels;
using System.Data.Entity;

namespace KartRacingManager.Interfaces.Data
{
    public interface ILogsDbContext : IDbContext
    {
        IDbSet<Log> Logs { get; set; }

        IDbSet<LogType> LogTypes { get; set; }
    }
}
