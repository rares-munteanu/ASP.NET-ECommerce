namespace WeShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedOrderStatusToOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "OrderStatus", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "OrderStatus");
        }
    }
}
