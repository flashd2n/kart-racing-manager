using System;
using System.Linq;
using KarRacingManager.Models.SqliteModels;
using KartRacingManager.Data.Interfaces;
using KartRacingManager.Interfaces.Logger;

namespace KartRacingManager.Logger
{
    public class SqliteLogger : ILogger
    {
        private readonly ILogsUnitOfWork logsUnitOfWork;

        public SqliteLogger(ILogsUnitOfWork logsUnitOfWork)
        {
            this.logsUnitOfWork = logsUnitOfWork;
        }

        public void Log(string logType, string logMessage, DateTime? logTime)
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
