namespace WeShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddShoppingCart : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ShoppingCarts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Items", "ShoppingCartId", c => c.Int(nullable: false));
            CreateIndex("dbo.Items", "ShoppingCartId");
            AddForeignKey("dbo.Items", "ShoppingCartId", "dbo.ShoppingCarts", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Items", "ShoppingCartId", "dbo.ShoppingCarts");
            DropIndex("dbo.Items", new[] { "ShoppingCartId" });
            DropColumn("dbo.Items", "ShoppingCartId");
            DropTable("dbo.ShoppingCarts");
        }
    }
}
