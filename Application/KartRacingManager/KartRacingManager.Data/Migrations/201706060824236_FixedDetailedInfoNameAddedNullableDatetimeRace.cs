namespace KartRacingManager.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixedDetailedInfoNameAddedNullableDatetimeRace : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.DetailedRacersInformations", newName: "DetailedRacersInformation");
            AlterColumn("dbo.Races", "StartTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Races", "StartTime", c => c.DateTime(nullable: false));
            RenameTable(name: "dbo.DetailedRacersInformation", newName: "DetailedRacersInformations");
        }
    }
}
