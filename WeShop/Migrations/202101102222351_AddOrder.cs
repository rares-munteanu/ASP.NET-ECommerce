namespace WeShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOrder : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PaymentTypeId = c.Byte(nullable: false),
                        DeliveryAddress_Id = c.Int(nullable: false),
                        User_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.DeliveryAddress_Id, cascadeDelete: true)
                .ForeignKey("dbo.PaymentTypes", t => t.PaymentTypeId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.PaymentTypeId)
                .Index(t => t.DeliveryAddress_Id)
                .Index(t => t.User_Id);
            
            AddColumn("dbo.Products", "Order_Id", c => c.Int());
            CreateIndex("dbo.Products", "Order_Id");
            AddForeignKey("dbo.Products", "Order_Id", "dbo.Orders", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Products", "Order_Id", "dbo.Orders");
            DropForeignKey("dbo.Orders", "PaymentTypeId", "dbo.PaymentTypes");
            DropForeignKey("dbo.Orders", "DeliveryAddress_Id", "dbo.Addresses");
            DropIndex("dbo.Products", new[] { "Order_Id" });
            DropIndex("dbo.Orders", new[] { "User_Id" });
            DropIndex("dbo.Orders", new[] { "DeliveryAddress_Id" });
            DropIndex("dbo.Orders", new[] { "PaymentTypeId" });
            DropColumn("dbo.Products", "Order_Id");
            DropTable("dbo.Orders");
        }
    }
}
