namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class start : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BobbinDebit",
                c => new
                    {
                        BobbinDebitId = c.Int(nullable: false, identity: true),
                        StartIndex = c.Int(nullable: false),
                        EndIndex = c.Int(nullable: false),
                        AmountUsed = c.Int(nullable: false),
                        WorkplaceId = c.Int(nullable: false),
                        BobbinId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BobbinDebitId)
                .ForeignKey("dbo.Bobbin", t => t.BobbinId, cascadeDelete: true)
                .ForeignKey("dbo.workplace", t => t.WorkplaceId, cascadeDelete: true)
                .Index(t => t.WorkplaceId)
                .Index(t => t.BobbinId);
            
            CreateTable(
                "dbo.Bobbin",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BobbinId = c.String(nullable: false, maxLength: 45, storeType: "nvarchar"),
                        CableLength = c.Int(nullable: false),
                        IsReturned = c.Boolean(nullable: false),
                        FetchDate = c.DateTime(nullable: false, precision: 0),
                        AmountRemains = c.Int(nullable: false),
                        ReturnDate = c.DateTime(precision: 0),
                        CableTypeId = c.Int(nullable: false),
                        FetchLocation = c.String(nullable: false, unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CableType", t => t.CableTypeId, cascadeDelete: true)
                .Index(t => t.BobbinId, unique: true)
                .Index(t => t.CableTypeId);
            
            CreateTable(
                "dbo.CableType",
                c => new
                    {
                        CableTypeId = c.Int(nullable: false, identity: true),
                        Description = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.CableTypeId);
            
            CreateTable(
                "dbo.workplace",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WorkplaceId = c.String(nullable: false, maxLength: 45, storeType: "nvarchar"),
                        Address = c.String(nullable: false, unicode: false),
                        CityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.City", t => t.CityId, cascadeDelete: true)
                .Index(t => t.WorkplaceId, unique: true)
                .Index(t => t.CityId);
            
            CreateTable(
                "dbo.City",
                c => new
                    {
                        CityId = c.Int(nullable: false, identity: true),
                        Zipcode = c.String(nullable: false, unicode: false),
                        Name = c.String(nullable: false, unicode: false),
                        Up = c.String(unicode: false),
                        structure = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.CityId);
            
            CreateTable(
                "dbo.Debit",
                c => new
                    {
                        DebitId = c.Int(nullable: false, identity: true),
                        Amount = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false, precision: 0),
                        WorkplaceId = c.Int(nullable: false),
                        ItemId = c.Int(nullable: false),
                        Debited_By_Staff_Id = c.Int(nullable: false),
                        Approved_By_Staff_Id = c.Int(nullable: false),
                        DebitState = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DebitId)
                .ForeignKey("dbo.Staff", t => t.Approved_By_Staff_Id, cascadeDelete: true)
                .ForeignKey("dbo.Staff", t => t.Debited_By_Staff_Id, cascadeDelete: true)
                .ForeignKey("dbo.Item", t => t.ItemId, cascadeDelete: true)
                .ForeignKey("dbo.workplace", t => t.WorkplaceId, cascadeDelete: true)
                .Index(t => t.WorkplaceId)
                .Index(t => t.ItemId)
                .Index(t => t.Debited_By_Staff_Id)
                .Index(t => t.Approved_By_Staff_Id);
            
            CreateTable(
                "dbo.Staff",
                c => new
                    {
                        StaffId = c.Int(nullable: false, identity: true),
                        Firstname = c.String(unicode: false),
                        Lastname = c.String(unicode: false),
                        ApplicationUserId = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.StaffId);
            
            CreateTable(
                "dbo.Item",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ItemId = c.String(nullable: false, maxLength: 45, storeType: "nvarchar"),
                        Description = c.String(nullable: false, unicode: false),
                        Amount = c.Int(nullable: false),
                        Image = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.ItemId, unique: true);
            
            CreateTable(
                "dbo.Deposit",
                c => new
                    {
                        DepositId = c.Int(nullable: false, identity: true),
                        ItemId = c.Int(nullable: false),
                        Amount = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false, precision: 0),
                        Deposited_By = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DepositId)
                .ForeignKey("dbo.Staff", t => t.Deposited_By, cascadeDelete: true)
                .ForeignKey("dbo.Item", t => t.ItemId, cascadeDelete: true)
                .Index(t => t.ItemId)
                .Index(t => t.Deposited_By);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Deposit", "ItemId", "dbo.Item");
            DropForeignKey("dbo.Deposit", "Deposited_By", "dbo.Staff");
            DropForeignKey("dbo.BobbinDebit", "WorkplaceId", "dbo.workplace");
            DropForeignKey("dbo.Debit", "WorkplaceId", "dbo.workplace");
            DropForeignKey("dbo.Debit", "ItemId", "dbo.Item");
            DropForeignKey("dbo.Debit", "Debited_By_Staff_Id", "dbo.Staff");
            DropForeignKey("dbo.Debit", "Approved_By_Staff_Id", "dbo.Staff");
            DropForeignKey("dbo.workplace", "CityId", "dbo.City");
            DropForeignKey("dbo.BobbinDebit", "BobbinId", "dbo.Bobbin");
            DropForeignKey("dbo.Bobbin", "CableTypeId", "dbo.CableType");
            DropIndex("dbo.Deposit", new[] { "Deposited_By" });
            DropIndex("dbo.Deposit", new[] { "ItemId" });
            DropIndex("dbo.Item", new[] { "ItemId" });
            DropIndex("dbo.Debit", new[] { "Approved_By_Staff_Id" });
            DropIndex("dbo.Debit", new[] { "Debited_By_Staff_Id" });
            DropIndex("dbo.Debit", new[] { "ItemId" });
            DropIndex("dbo.Debit", new[] { "WorkplaceId" });
            DropIndex("dbo.workplace", new[] { "CityId" });
            DropIndex("dbo.workplace", new[] { "WorkplaceId" });
            DropIndex("dbo.Bobbin", new[] { "CableTypeId" });
            DropIndex("dbo.Bobbin", new[] { "BobbinId" });
            DropIndex("dbo.BobbinDebit", new[] { "BobbinId" });
            DropIndex("dbo.BobbinDebit", new[] { "WorkplaceId" });
            DropTable("dbo.Deposit");
            DropTable("dbo.Item");
            DropTable("dbo.Staff");
            DropTable("dbo.Debit");
            DropTable("dbo.City");
            DropTable("dbo.workplace");
            DropTable("dbo.CableType");
            DropTable("dbo.Bobbin");
            DropTable("dbo.BobbinDebit");
        }
    }
}
