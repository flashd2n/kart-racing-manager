namespace KartRacingManager.Data.PostgreSqlMigrations
{
    using System.Data.Entity.Migrations;

    public sealed class KartsDbConfig : DbMigrationsConfiguration<KartDbContext>
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
