using System;

namespace KartRacingManager.Interfaces.Logger
{
    public interface ILogger
    {
        void Log(string logType, string logMessage);
    }
}
