namespace KartRacingManager.Data.SqliteMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_log_message : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Logs", "Message", c => c.String(maxLength: 2147483647));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Logs", "Message");
        }
    }
}
