namespace WeShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProductItem : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Description = c.String(),
                        Price = c.Int(nullable: false),
                        ImagePath = c.String(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ProductItems");
        }
    }
}
