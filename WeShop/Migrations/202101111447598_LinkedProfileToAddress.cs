namespace WeShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LinkedProfileToAddress : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Profiles");
            AddColumn("dbo.Addresses", "ProfileId", c => c.Int(nullable: false));
            AlterColumn("dbo.Profiles", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Profiles", "Id");
            CreateIndex("dbo.Profiles", "Id");
            AddForeignKey("dbo.Profiles", "Id", "dbo.Addresses", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Profiles", "Id", "dbo.Addresses");
            DropIndex("dbo.Profiles", new[] { "Id" });
            DropPrimaryKey("dbo.Profiles");
            AlterColumn("dbo.Profiles", "Id", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.Addresses", "ProfileId");
            AddPrimaryKey("dbo.Profiles", "Id");
        }
    }
}
