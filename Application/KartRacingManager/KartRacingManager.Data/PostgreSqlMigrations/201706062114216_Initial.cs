namespace KartRacingManager.Data.PostgreSqlMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            MoveTable(name: "dbo.Karts", newSchema: "public");
            MoveTable(name: "dbo.TransmissionTypes", newSchema: "public");
        }
        
        public override void Down()
        {
            MoveTable(name: "public.TransmissionTypes", newSchema: "dbo");
            MoveTable(name: "public.Karts", newSchema: "dbo");
        }
    }
}
