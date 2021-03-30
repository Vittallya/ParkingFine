namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class start2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Evacuations", "CarStatus", c => c.Int(nullable: false));
            DropColumn("dbo.Autoes", "CarStatus");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Autoes", "CarStatus", c => c.Int(nullable: false));
            DropColumn("dbo.Evacuations", "CarStatus");
        }
    }
}
