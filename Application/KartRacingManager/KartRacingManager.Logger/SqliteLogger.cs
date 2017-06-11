using System;
using System.Linq;
using KarRacingManager.Models.SqliteModels;
using KartRacingManager.Data.Interfaces;
using KartRacingManager.Interfaces.Logger;
using KartRacingManager.Interfaces.Providers;

namespace KartRacingManager.Logger
{
    public class SqliteLogger : ILogger
    {
        private readonly ILogsUnitOfWork logsUnitOfWork;
        private readonly IDateTimeProvider datetimeProvider;

        public SqliteLogger(ILogsUnitOfWork logsUnitOfWork, IDateTimeProvider datetimeProvider)
        {
            this.logsUnitOfWork = logsUnitOfWork;
            this.datetimeProvider = datetimeProvider;
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
                logTypeObj = new LogType {Name = logType};
            }

            var logTime = this.datetimeProvider.UtcNow;

            var log = new Log
            {
                LogType = logTypeObj,
                Message = logMessage,
                TimeStamp = logTime
            };

            this.logsUnitOfWork.LogsRepo.Add(log);
            this.logsUnitOfWork.Save();
        }
    }
}
