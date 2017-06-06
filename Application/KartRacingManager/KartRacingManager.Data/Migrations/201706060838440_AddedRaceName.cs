namespace KartRacingManager.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRaceName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Races", "Name", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Races", "Name");
        }
    }
}
