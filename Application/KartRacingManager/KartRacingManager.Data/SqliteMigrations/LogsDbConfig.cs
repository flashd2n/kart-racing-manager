namespace KartRacingManager.Data.SqliteMigrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.SQLite.EF6.Migrations;
    using System.Linq;

    internal sealed class LogsDbConfig : DbMigrationsConfiguration<KartRacingManager.Data.LogsDbContext>
    {
        public LogsDbConfig()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"SqliteMigrations";
            SetSqlGenerator("System.Data.SQLite", new SQLiteMigrationSqlGenerator());
        }

        protected override void Seed(KartRacingManager.Data.LogsDbContext context)
        {
        }
    }
}
