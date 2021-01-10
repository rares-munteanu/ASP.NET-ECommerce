namespace WeShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LinkedUserToProfile : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Profiles");
            AddColumn("dbo.AspNetUsers", "ProfileId", c => c.Int(nullable: false));
            AlterColumn("dbo.Profiles", "Id", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Profiles", "Id");
            CreateIndex("dbo.Profiles", "Id");
            AddForeignKey("dbo.Profiles", "Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Profiles", "Id", "dbo.AspNetUsers");
            DropIndex("dbo.Profiles", new[] { "Id" });
            DropPrimaryKey("dbo.Profiles");
            AlterColumn("dbo.Profiles", "Id", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.AspNetUsers", "ProfileId");
            AddPrimaryKey("dbo.Profiles", "Id");
        }
    }
}
