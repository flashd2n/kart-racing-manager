using KarRacingManager.Models.SqliteModels;
using KartRacingManager.Data.Interfaces;
using System.Data.Entity;
using System.Linq;

namespace KartRacingManager.Data.Repositories.LogsDbRepositories
{
    public class LogsRepository
    {
        private ILogsDbContext context;

        public LogsRepository(ILogsDbContext context)
        {
            this.context = context;
        }

        public IQueryable<Log> All
        {
            get { return this.context.Logs; }
        }

        public void Add(Log entity)
        {
            this.context.Logs.Add(entity);
        }

        public Log FindById(object id)
        {
            return this.context.Logs.Find(id);
        }

        public void Remove(Log entity)
        {
            this.context.Logs.Remove(entity);
        }

        public void Update(Log entity)
        {
            this.ChangeState(entity, EntityState.Modified);
        }

        private void ChangeState(Log entity, EntityState state)
        {
            var dbEntry = this.context.Entry(entity);
            dbEntry.State = state;
        }
    }
}
