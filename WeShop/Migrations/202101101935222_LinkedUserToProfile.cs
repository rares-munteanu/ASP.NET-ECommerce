namespace WeShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LinkedUserToProfile : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Profiles", "User_Id", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Profiles", "User_Id");
            AddForeignKey("dbo.Profiles", "User_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Profiles", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Profiles", new[] { "User_Id" });
            DropColumn("dbo.Profiles", "User_Id");
        }
    }
}
