namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Start1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Autoes", "CarStatus", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Autoes", "CarStatus");
        }
    }
}
