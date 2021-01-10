using System.Data.Entity.Migrations;

namespace WeShop.Migrations
{
    public partial class AddProfile : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                    "dbo.Profiles",
                    c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 40),
                        LastName = c.String(nullable: false, maxLength: 40),
                        Phone = c.String(nullable: false, maxLength: 10),
                        Birthdate = c.DateTime(nullable: false)
                    })
                .PrimaryKey(t => t.Id);
        }

        public override void Down()
        {
            DropTable("dbo.Profiles");
        }
    }
}