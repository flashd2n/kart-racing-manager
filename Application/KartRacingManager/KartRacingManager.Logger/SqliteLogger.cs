using System.Linq;
using KartRacingManager.Data.Interfaces;
using KartRacingManager.Interfaces.Logger;
using KartRacingManager.Interfaces.Providers;
using KarRacingManager.Models;
using Bytes2you.Validation;

namespace KartRacingManager.Logger
{
    public class SqliteLogger : ILogger
    {
        private readonly ILogsUnitOfWork logsUnitOfWork;
        private readonly IDateTimeProvider datetimeProvider;
        private readonly IModelFactory modelFactory;

        public SqliteLogger(ILogsUnitOfWork logsUnitOfWork, IDateTimeProvider datetimeProvider, IModelFactory modelFactory)
        {
            Guard.WhenArgument(logsUnitOfWork, "logsUnitOfWork").IsNull().Throw();
            Guard.WhenArgument(datetimeProvider, "datetimeProvider").IsNull().Throw();
            Guard.WhenArgument(modelFactory, "modelFactory").IsNull().Throw();

            this.logsUnitOfWork = logsUnitOfWork;
            this.datetimeProvider = datetimeProvider;
            this.modelFactory = modelFactory;
        }

        public void Log(string logType, string logMessage)
        {
            if (logType == null)
            {
                logType = "info";
            }

            if (logMessage == null)
            {
                logMessage = "";
            }

            var logTypeObj = logsUnitOfWork.LogTypesRepo.All.SingleOrDefault(lt => lt.Name == logType);
            if (logTypeObj == null)
            {
                logTypeObj = this.modelFactory.CreateLogType();
                logTypeObj.Name = logType;
            }

            var logTime = this.datetimeProvider.UtcNow;

            var log = this.modelFactory.CreateLog();

            log.LogType = logTypeObj;
            log.Message = logMessage;
            log.TimeStamp = logTime;

            this.logsUnitOfWork.LogsRepo.Add(log);
            this.logsUnitOfWork.Save();
        }
    }
}
