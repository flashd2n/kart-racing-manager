namespace KartRacingManager.Data.SqliteMigrations
{
    using System.Data.Entity.Migrations;
    using System.Data.SQLite.EF6.Migrations;

    internal sealed class LogsDbConfig : DbMigrationsConfiguration<LogsDbContext>
    {
        public LogsDbConfig()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"SqliteMigrations";
            SetSqlGenerator("System.Data.SQLite", new SQLiteMigrationSqlGenerator());
        }

        protected override void Seed(LogsDbContext context)
        {
        }
    }
}
