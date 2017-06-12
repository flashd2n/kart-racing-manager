using Bytes2you.Validation;
using KartRacingManager.Data.Interfaces;
using KartRacingManager.Data.Repositories.LogsDbRepositories;

namespace KartRacingManager.Data
{
    public class LogsUnitOfWork : ILogsUnitOfWork, IUnitOfWork
    {
        private ILogsDbContext context;

        private LogsRepository logsRepo;
        private LogTypesRepository logTypesRepo;

        public LogsUnitOfWork(ILogsDbContext context)
        {
            Guard.WhenArgument(context, "context").IsNull().Throw();

            this.context = context;
        }

        public LogsRepository LogsRepo
        {
            get
            {
                if (this.logsRepo == null)
                {
                    this.logsRepo = new LogsRepository(this.context);
                }
                return this.logsRepo;
            }
        }

        public LogTypesRepository LogTypesRepo
        {
            get
            {
                if (this.logTypesRepo == null)
                {
                    this.logTypesRepo = new LogTypesRepository(this.context);
                }
                return this.logTypesRepo;
            }
        }

        public void Dispose()
        {
            this.context.Dispose();
        }

        public void Save()
        {
            this.context.SaveChanges();
        }
    }
}
