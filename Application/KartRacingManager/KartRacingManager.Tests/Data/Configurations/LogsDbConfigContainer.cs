using KartRacingManager.Data.SqliteMigrations;

namespace KartRacingManager.Tests.Data.Configurations
{
    public class LogsDbConfigContainer
    {
        public LogsDbConfigContainer(LogsDbConfig config)
        {
            this.Config = config;
        }

        public LogsDbConfig Config { get; set; }
    }
}
