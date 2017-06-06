namespace KartRacingManager.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetRacerConstraints : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Racers", "FirstName", c => c.String(nullable: false, maxLength: 60));
            AlterColumn("dbo.Racers", "LastName", c => c.String(nullable: false, maxLength: 60));
            AlterColumn("dbo.DetailedRacersInformations", "Weight", c => c.Double());
            AlterColumn("dbo.DetailedRacersInformations", "Height", c => c.Double());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DetailedRacersInformations", "Height", c => c.Double(nullable: false));
            AlterColumn("dbo.DetailedRacersInformations", "Weight", c => c.Double(nullable: false));
            AlterColumn("dbo.Racers", "LastName", c => c.String());
            AlterColumn("dbo.Racers", "FirstName", c => c.String());
        }
    }
}
