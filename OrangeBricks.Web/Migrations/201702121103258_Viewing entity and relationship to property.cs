namespace OrangeBricks.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Viewingentityandrelationshiptoproperty : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Viewings", "PropertyId");
            AddForeignKey("dbo.Viewings", "PropertyId", "dbo.Properties", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Viewings", "PropertyId", "dbo.Properties");
            DropIndex("dbo.Viewings", new[] { "PropertyId" });
        }
    }
}
