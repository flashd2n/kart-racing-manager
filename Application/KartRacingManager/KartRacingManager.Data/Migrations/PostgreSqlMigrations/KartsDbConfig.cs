namespace KartRacingManager.Data.PostgreSqlMigrations
{
    using System.Data.Entity.Migrations;

    internal sealed class KartsDbConfig : DbMigrationsConfiguration<KartDbContext>
    {
        public KartsDbConfig()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"PostgreSqlMigrations";
        }

        protected override void Seed(KartDbContext context)
        {
        }
    }
}
