using System.Data.Entity.Migrations;

namespace KartRacingManager.Data.Migrations
{
    public sealed class MainDbConfig : DbMigrationsConfiguration<MainDbContext>
    {
        public MainDbConfig()
        {
            this.AutomaticMigrationsEnabled = false;
            this.AutomaticMigrationDataLossAllowed = false;
            MigrationsDirectory = @"SqlServerMigrations";
        }

        protected override void Seed(MainDbContext context)
        {
        }
    }
}
