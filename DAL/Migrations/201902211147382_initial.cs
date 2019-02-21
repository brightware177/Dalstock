namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bobbin", "InfrastructureId", c => c.Int(nullable: false));
            CreateIndex("dbo.Bobbin", "InfrastructureId");
            AddForeignKey("dbo.Bobbin", "InfrastructureId", "dbo.Infrastructure", "InfrastructureId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bobbin", "InfrastructureId", "dbo.Infrastructure");
            DropIndex("dbo.Bobbin", new[] { "InfrastructureId" });
            DropColumn("dbo.Bobbin", "InfrastructureId");
        }
    }
}
