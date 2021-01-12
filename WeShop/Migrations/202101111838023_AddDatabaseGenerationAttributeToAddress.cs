namespace WeShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDatabaseGenerationAttributeToAddress : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Addresses", new[] { "Id" });
            DropPrimaryKey("dbo.Addresses");
            AlterColumn("dbo.Addresses", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Addresses", "Id");
            CreateIndex("dbo.Addresses", "Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Addresses", new[] { "Id" });
            DropPrimaryKey("dbo.Addresses");
            AlterColumn("dbo.Addresses", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Addresses", "Id");
            CreateIndex("dbo.Addresses", "Id");
        }
    }
}
