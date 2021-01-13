namespace WeShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LinkedOrderToProductItem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductItems", "Order_Id", c => c.Int());
            CreateIndex("dbo.ProductItems", "Order_Id");
            AddForeignKey("dbo.ProductItems", "Order_Id", "dbo.Orders", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductItems", "Order_Id", "dbo.Orders");
            DropIndex("dbo.ProductItems", new[] { "Order_Id" });
            DropColumn("dbo.ProductItems", "Order_Id");
        }
    }
}
