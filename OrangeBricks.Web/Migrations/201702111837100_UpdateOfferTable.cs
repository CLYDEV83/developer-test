namespace OrangeBricks.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateOfferTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Offers", "Property_Id", "dbo.Properties");
            DropIndex("dbo.Offers", new[] { "Property_Id" });
            RenameColumn(table: "dbo.Offers", name: "Property_Id", newName: "propertyId");
            AddColumn("dbo.Offers", "BuyerUserId", c => c.String());
            AlterColumn("dbo.Offers", "propertyId", c => c.Int(nullable: false));
            CreateIndex("dbo.Offers", "propertyId");
            AddForeignKey("dbo.Offers", "propertyId", "dbo.Properties", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Offers", "propertyId", "dbo.Properties");
            DropIndex("dbo.Offers", new[] { "propertyId" });
            AlterColumn("dbo.Offers", "propertyId", c => c.Int());
            DropColumn("dbo.Offers", "BuyerUserId");
            RenameColumn(table: "dbo.Offers", name: "propertyId", newName: "Property_Id");
            CreateIndex("dbo.Offers", "Property_Id");
            AddForeignKey("dbo.Offers", "Property_Id", "dbo.Properties", "Id");
        }
    }
}
