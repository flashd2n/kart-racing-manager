namespace KartRacingManager.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedLapsAndTracks : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Laps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartTime = c.DateTime(nullable: false),
                        FinishTime = c.DateTime(nullable: false),
                        RacerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Racers", t => t.RacerId, cascadeDelete: true)
                .Index(t => t.RacerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Laps", "RacerId", "dbo.Racers");
            DropIndex("dbo.Laps", new[] { "RacerId" });
            DropTable("dbo.Laps");
        }
    }
}
