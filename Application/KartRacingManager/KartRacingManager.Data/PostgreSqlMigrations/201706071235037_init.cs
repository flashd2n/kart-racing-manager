namespace KartRacingManager.Data.PostgreSqlMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "public.Karts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HorsePower = c.Int(nullable: false),
                        TopSpeedKph = c.Double(nullable: false),
                        TransmissionTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("public.TransmissionTypes", t => t.TransmissionTypeId, cascadeDelete: true)
                .Index(t => t.TransmissionTypeId);
            
            CreateTable(
                "public.TransmissionTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("public.Karts", "TransmissionTypeId", "public.TransmissionTypes");
            DropIndex("public.Karts", new[] { "TransmissionTypeId" });
            DropTable("public.TransmissionTypes");
            DropTable("public.Karts");
        }
    }
}
