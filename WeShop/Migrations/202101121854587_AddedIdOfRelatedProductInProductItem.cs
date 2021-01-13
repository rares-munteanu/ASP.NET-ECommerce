namespace WeShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedIdOfRelatedProductInProductItem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductItems", "IdOfRelatedProduct", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductItems", "IdOfRelatedProduct");
        }
    }
}
