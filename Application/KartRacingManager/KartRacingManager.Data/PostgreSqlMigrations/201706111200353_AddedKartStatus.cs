namespace KartRacingManager.Data.PostgreSqlMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedKartStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("public.Karts", "KartStatus", c => c.Int(nullable: false, defaultValue: 0));
        }
        
        public override void Down()
        {
            DropColumn("public.Karts", "KartStatus");
        }
    }
}
