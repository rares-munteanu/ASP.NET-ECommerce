namespace WeShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LinkedProfileToAddresses : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Addresses", "ProfileId", c => c.Int(nullable: false));
            CreateIndex("dbo.Addresses", "ProfileId");
            AddForeignKey("dbo.Addresses", "ProfileId", "dbo.Profiles", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Addresses", "ProfileId", "dbo.Profiles");
            DropIndex("dbo.Addresses", new[] { "ProfileId" });
            DropColumn("dbo.Addresses", "ProfileId");
        }
    }
}
