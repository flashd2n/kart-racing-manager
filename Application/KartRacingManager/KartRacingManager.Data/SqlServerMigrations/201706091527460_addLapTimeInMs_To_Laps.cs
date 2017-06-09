namespace KartRacingManager.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addLapTimeInMs_To_Laps : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Laps", "LapTimeInMs", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Laps", "LapTimeInMs");
        }
    }
}
