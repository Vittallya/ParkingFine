namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class final : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Declarations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EvacuationId = c.Int(nullable: false),
                        CreatingDate = c.DateTime(nullable: false),
                        ComingDate = c.DateTime(nullable: false),
                        DeclayerType = c.Int(nullable: false),
                        PayingType = c.Int(nullable: false),
                        DecStatus = c.Int(nullable: false),
                        ProfileId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Evacuations", t => t.EvacuationId, cascadeDelete: true)
                .ForeignKey("dbo.Profiles", t => t.ProfileId, cascadeDelete: true)
                .Index(t => t.EvacuationId)
                .Index(t => t.ProfileId);
            
            CreateTable(
                "dbo.Profiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FIO = c.String(),
                        DriverLicence = c.String(),
                        Osago = c.String(),
                        PasportNumber = c.String(),
                        PasportSerial = c.String(),
                        PhoneNumber = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Declarations", "ProfileId", "dbo.Profiles");
            DropForeignKey("dbo.Declarations", "EvacuationId", "dbo.Evacuations");
            DropIndex("dbo.Declarations", new[] { "ProfileId" });
            DropIndex("dbo.Declarations", new[] { "EvacuationId" });
            DropTable("dbo.Profiles");
            DropTable("dbo.Declarations");
        }
    }
}
