namespace KartRacingManager.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLapRaceRelation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Laps", "RaceId", c => c.Int());
            CreateIndex("dbo.Laps", "RaceId");
            AddForeignKey("dbo.Laps", "RaceId", "dbo.Races", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Laps", "RaceId", "dbo.Races");
            DropIndex("dbo.Laps", new[] { "RaceId" });
            DropColumn("dbo.Laps", "RaceId");
        }
    }
}
