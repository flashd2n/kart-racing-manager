namespace KartRacingManager.Data.PostgreSqlMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class transmision_type_name_from_int_to_string : DbMigration
    {
        public override void Up()
        {
            AlterColumn("public.TransmissionTypes", "Name", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("public.TransmissionTypes", "Name", c => c.Int(nullable: false));
        }
    }
}
