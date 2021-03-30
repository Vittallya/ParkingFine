namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Start : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Autoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GovNumber = c.String(),
                        Mark = c.String(),
                        Model = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Evacuations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FineId = c.Int(nullable: false),
                        AutoId = c.Int(nullable: false),
                        ParkingId = c.Int(nullable: false),
                        PlaceEvac = c.String(),
                        DateTimeEvac = c.DateTime(nullable: false),
                        EvacCost = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Autoes", t => t.AutoId, cascadeDelete: true)
                .ForeignKey("dbo.Fines", t => t.FineId, cascadeDelete: true)
                .ForeignKey("dbo.Parkings", t => t.ParkingId, cascadeDelete: true)
                .Index(t => t.FineId)
                .Index(t => t.AutoId)
                .Index(t => t.ParkingId);
            
            CreateTable(
                "dbo.Fines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Cost = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Parkings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Address = c.String(),
                        CostByHour = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Evacuations", "ParkingId", "dbo.Parkings");
            DropForeignKey("dbo.Evacuations", "FineId", "dbo.Fines");
            DropForeignKey("dbo.Evacuations", "AutoId", "dbo.Autoes");
            DropIndex("dbo.Evacuations", new[] { "ParkingId" });
            DropIndex("dbo.Evacuations", new[] { "AutoId" });
            DropIndex("dbo.Evacuations", new[] { "FineId" });
            DropTable("dbo.Parkings");
            DropTable("dbo.Fines");
            DropTable("dbo.Evacuations");
            DropTable("dbo.Autoes");
        }
    }
}
