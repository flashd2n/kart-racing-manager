namespace KartRacingManager.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRaces : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tracks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Length = c.Double(nullable: false),
                        AddressId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.AddressId)
                .Index(t => t.AddressId);
            
            CreateTable(
                "dbo.Races",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        LapCount = c.Int(nullable: false),
                        TrackId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tracks", t => t.TrackId, cascadeDelete: true)
                .Index(t => t.TrackId);
            
            CreateTable(
                "dbo.RaceRacers",
                c => new
                    {
                        Race_Id = c.Int(nullable: false),
                        Racer_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Race_Id, t.Racer_Id })
                .ForeignKey("dbo.Races", t => t.Race_Id, cascadeDelete: true)
                .ForeignKey("dbo.Racers", t => t.Racer_Id, cascadeDelete: true)
                .Index(t => t.Race_Id)
                .Index(t => t.Racer_Id);
            
            AddColumn("dbo.Laps", "TrackId", c => c.Int(nullable: false));
            CreateIndex("dbo.Laps", "TrackId");
            AddForeignKey("dbo.Laps", "TrackId", "dbo.Tracks", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Races", "TrackId", "dbo.Tracks");
            DropForeignKey("dbo.RaceRacers", "Racer_Id", "dbo.Racers");
            DropForeignKey("dbo.RaceRacers", "Race_Id", "dbo.Races");
            DropForeignKey("dbo.Laps", "TrackId", "dbo.Tracks");
            DropForeignKey("dbo.Tracks", "AddressId", "dbo.Addresses");
            DropIndex("dbo.RaceRacers", new[] { "Racer_Id" });
            DropIndex("dbo.RaceRacers", new[] { "Race_Id" });
            DropIndex("dbo.Races", new[] { "TrackId" });
            DropIndex("dbo.Tracks", new[] { "AddressId" });
            DropIndex("dbo.Laps", new[] { "TrackId" });
            DropColumn("dbo.Laps", "TrackId");
            DropTable("dbo.RaceRacers");
            DropTable("dbo.Races");
            DropTable("dbo.Tracks");
        }
    }
}
