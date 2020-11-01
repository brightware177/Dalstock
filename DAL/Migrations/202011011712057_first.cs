namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CablePlacement",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CableLength = c.Int(nullable: false),
                        CableTypeId = c.Int(nullable: false),
                        Workplace_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CableType", t => t.CableTypeId, cascadeDelete: true)
                .ForeignKey("dbo.workplace", t => t.Workplace_Id)
                .Index(t => t.CableTypeId)
                .Index(t => t.Workplace_Id);
            
            CreateTable(
                "dbo.FilePath",
                c => new
                    {
                        FilePathId = c.Int(nullable: false, identity: true),
                        FileName = c.String(maxLength: 255, storeType: "nvarchar"),
                        CablePlacementId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FilePathId)
                .ForeignKey("dbo.CablePlacement", t => t.CablePlacementId, cascadeDelete: true)
                .Index(t => t.CablePlacementId);
            
            AddColumn("dbo.BobbinDebit", "Date", c => c.DateTime(nullable: false, precision: 0));
            AddColumn("dbo.Bobbin", "BobbinEmailDate", c => c.DateTime(precision: 0));
            AddColumn("dbo.Bobbin", "InfrastructureId", c => c.Int(nullable: false));
            AddColumn("dbo.workplace", "WorkplaceEmailDate", c => c.DateTime(precision: 0));
            AddColumn("dbo.workplace", "EstimatedStart", c => c.DateTime(precision: 0));
            AddColumn("dbo.workplace", "EstimatedEnd", c => c.DateTime(precision: 0));
            AddColumn("dbo.workplace", "StartDate", c => c.DateTime(precision: 0));
            AddColumn("dbo.workplace", "EndDate", c => c.DateTime(precision: 0));
            AddColumn("dbo.workplace", "AfterCareEmailDate", c => c.DateTime(precision: 0));
            AlterColumn("dbo.Bobbin", "FetchLocation", c => c.String(unicode: false));
            CreateIndex("dbo.Bobbin", "InfrastructureId");
            AddForeignKey("dbo.Bobbin", "InfrastructureId", "dbo.Infrastructure", "InfrastructureId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CablePlacement", "Workplace_Id", "dbo.workplace");
            DropForeignKey("dbo.FilePath", "CablePlacementId", "dbo.CablePlacement");
            DropForeignKey("dbo.CablePlacement", "CableTypeId", "dbo.CableType");
            DropForeignKey("dbo.Bobbin", "InfrastructureId", "dbo.Infrastructure");
            DropIndex("dbo.FilePath", new[] { "CablePlacementId" });
            DropIndex("dbo.CablePlacement", new[] { "Workplace_Id" });
            DropIndex("dbo.CablePlacement", new[] { "CableTypeId" });
            DropIndex("dbo.Bobbin", new[] { "InfrastructureId" });
            AlterColumn("dbo.Bobbin", "FetchLocation", c => c.String(nullable: false, unicode: false));
            DropColumn("dbo.workplace", "AfterCareEmailDate");
            DropColumn("dbo.workplace", "EndDate");
            DropColumn("dbo.workplace", "StartDate");
            DropColumn("dbo.workplace", "EstimatedEnd");
            DropColumn("dbo.workplace", "EstimatedStart");
            DropColumn("dbo.workplace", "WorkplaceEmailDate");
            DropColumn("dbo.Bobbin", "InfrastructureId");
            DropColumn("dbo.Bobbin", "BobbinEmailDate");
            DropColumn("dbo.BobbinDebit", "Date");
            DropTable("dbo.FilePath");
            DropTable("dbo.CablePlacement");
        }
    }
}
