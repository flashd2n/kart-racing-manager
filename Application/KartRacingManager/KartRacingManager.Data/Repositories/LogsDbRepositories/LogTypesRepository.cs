using KarRacingManager.Models.SqliteModels;
using KartRacingManager.Data.Interfaces;
using System.Data.Entity;
using System.Linq;

namespace KartRacingManager.Data.Repositories.LogsDbRepositories
{
    public class LogTypesRepository
    {
        private ILogsDbContext context;

        public LogTypesRepository(ILogsDbContext context)
        {
            this.context = context;
        }

        public IQueryable<LogType> All
        {
            get { return this.context.LogTypes; }
        }

        public void Add(LogType entity)
        {
            this.context.LogTypes.Add(entity);
        }

        public LogType FindById(object id)
        {
            return this.context.LogTypes.Find(id);
        }

        public void Remove(LogType entity)
        {
            this.context.LogTypes.Remove(entity);
        }

        public void Update(LogType entity)
        {
            this.ChangeState(entity, EntityState.Modified);
        }

        private void ChangeState(LogType entity, EntityState state)
        {
            var dbEntry = this.context.Entry(entity);
            dbEntry.State = state;
        }
    }
}
