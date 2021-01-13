namespace WeShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedOrderIdToProductItem : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductItems", "Order_Id", "dbo.Orders");
            DropIndex("dbo.ProductItems", new[] { "Order_Id" });
            RenameColumn(table: "dbo.ProductItems", name: "Order_Id", newName: "OrderId");
            AlterColumn("dbo.ProductItems", "OrderId", c => c.Int(nullable: false));
            CreateIndex("dbo.ProductItems", "OrderId");
            AddForeignKey("dbo.ProductItems", "OrderId", "dbo.Orders", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductItems", "OrderId", "dbo.Orders");
            DropIndex("dbo.ProductItems", new[] { "OrderId" });
            AlterColumn("dbo.ProductItems", "OrderId", c => c.Int());
            RenameColumn(table: "dbo.ProductItems", name: "OrderId", newName: "Order_Id");
            CreateIndex("dbo.ProductItems", "Order_Id");
            AddForeignKey("dbo.ProductItems", "Order_Id", "dbo.Orders", "Id");
        }
    }
}
