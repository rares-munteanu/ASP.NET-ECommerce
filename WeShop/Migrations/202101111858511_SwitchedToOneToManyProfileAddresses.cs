namespace WeShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SwitchedToOneToManyProfileAddresses : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Addresses", "Id", "dbo.Profiles");
            DropIndex("dbo.Addresses", new[] { "Id" });
            AddColumn("dbo.Addresses", "ProfileId", c => c.Int(nullable: false));
            CreateIndex("dbo.Addresses", "ProfileId");
            AddForeignKey("dbo.Addresses", "ProfileId", "dbo.Profiles", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Addresses", "ProfileId", "dbo.Profiles");
            DropIndex("dbo.Addresses", new[] { "ProfileId" });
            DropColumn("dbo.Addresses", "ProfileId");
            CreateIndex("dbo.Addresses", "Id");
            AddForeignKey("dbo.Addresses", "Id", "dbo.Profiles", "Id");
        }
    }
}
