namespace KartRacingManager.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAllConstraints : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Addresses", "Location", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Cities", "Name", c => c.String(nullable: false, maxLength: 60));
            AlterColumn("dbo.Countries", "Name", c => c.String(nullable: false, maxLength: 60));
            AlterColumn("dbo.Races", "EndTime", c => c.DateTime());
            AlterColumn("dbo.Tracks", "Name", c => c.String(nullable: false, maxLength: 60));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tracks", "Name", c => c.String());
            AlterColumn("dbo.Races", "EndTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Countries", "Name", c => c.String());
            AlterColumn("dbo.Cities", "Name", c => c.String());
            AlterColumn("dbo.Addresses", "Location", c => c.String());
        }
    }
}
