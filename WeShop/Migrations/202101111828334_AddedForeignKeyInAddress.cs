using System.Data.Entity.Migrations;

namespace WeShop.Migrations
{
    public partial class AddedForeignKeyInAddress : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Profiles", "Id", "dbo.Addresses");
            DropIndex("dbo.Profiles", new[] {"Id"});
            DropPrimaryKey("dbo.Addresses");
            DropPrimaryKey("dbo.Profiles");
            AlterColumn("dbo.Addresses", "Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Profiles", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Addresses", "Id");
            AddPrimaryKey("dbo.Profiles", "Id");
            CreateIndex("dbo.Addresses", "Id");
            AddForeignKey("dbo.Addresses", "Id", "dbo.Profiles", "Id");
            DropColumn("dbo.Addresses", "ProfileId");
        }

        public override void Down()
        {
            AddColumn("dbo.Addresses", "ProfileId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Addresses", "Id", "dbo.Profiles");
            DropIndex("dbo.Addresses", new[] {"Id"});
            DropPrimaryKey("dbo.Profiles");
            DropPrimaryKey("dbo.Addresses");
            AlterColumn("dbo.Profiles", "Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Addresses", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Profiles", "Id");
            AddPrimaryKey("dbo.Addresses", "Id");
            CreateIndex("dbo.Profiles", "Id");
            AddForeignKey("dbo.Profiles", "Id", "dbo.Addresses", "Id");
        }
    }
}