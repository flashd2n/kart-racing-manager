using KartRacingManager.Data.Migrations;

namespace KartRacingManager.Tests.Data.Configurations
{
    public class MainDbConfigContainer
    {
        public MainDbConfigContainer(MainDbConfig config)
        {
            this.Config = config;
        }

        public MainDbConfig Config { get; private set; }
    }
}
