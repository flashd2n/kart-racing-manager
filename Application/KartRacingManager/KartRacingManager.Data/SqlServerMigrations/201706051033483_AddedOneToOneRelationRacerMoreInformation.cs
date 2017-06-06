namespace KartRacingManager.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedOneToOneRelationRacerMoreInformation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DetailedRacersInformations",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Weight = c.Double(nullable: false),
                        Height = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Racers", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DetailedRacersInformations", "Id", "dbo.Racers");
            DropIndex("dbo.DetailedRacersInformations", new[] { "Id" });
            DropTable("dbo.DetailedRacersInformations");
        }
    }
}
