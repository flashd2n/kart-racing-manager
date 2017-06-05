namespace KartRacingManager.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRaceStatuses : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Races", "RaceStatus", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Races", "RaceStatus");
        }
    }
}
