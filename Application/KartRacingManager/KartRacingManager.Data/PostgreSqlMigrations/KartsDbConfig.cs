namespace KartRacingManager.Data.PostgreSqlMigrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class KartsDbConfig : DbMigrationsConfiguration<KartRacingManager.Data.KartDbContext>
    {
        public KartsDbConfig()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"PostgreSqlMigrations";
        }

        protected override void Seed(KartRacingManager.Data.KartDbContext context)
        {
        }
    }
}
