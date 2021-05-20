namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Parking_phone_city : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Parkings", "Phone", c => c.String());
            AddColumn("dbo.Parkings", "City", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Parkings", "City");
            DropColumn("dbo.Parkings", "Phone");
        }
    }
}
