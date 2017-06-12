namespace KartRacingManager.Data.SqliteMigrations
{
    using System.Data.Entity.Migrations;
    using System.Data.SQLite.EF6.Migrations;

    public sealed class LogsDbConfig : DbMigrationsConfiguration<LogsDbContext>
    {
        public LogsDbConfig()
        {
            this.AutomaticMigrationsEnabled = false;
            this.AutomaticMigrationDataLossAllowed = false;
            MigrationsDirectory = @"SqliteMigrations";
            SetSqlGenerator("System.Data.SQLite", new SQLiteMigrationSqlGenerator());
        }

        protected override void Seed(LogsDbContext context)
        {
        }
    }
}
