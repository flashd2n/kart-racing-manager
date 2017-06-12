using KartRacingManager.Data.PostgreSqlMigrations;

namespace KartRacingManager.Tests.Data.Configurations
{
    public class KartsDbConfigContainer
    {
        public KartsDbConfigContainer(KartsDbConfig config)
        {
            this.Config = config;
        }

        public KartsDbConfig Config { get; set; }
    }
}
