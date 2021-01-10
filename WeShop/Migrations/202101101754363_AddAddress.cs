using System.Data.Entity.Migrations;

namespace WeShop.Migrations
{
    public partial class AddAddress : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                    "dbo.Addresses",
                    c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        County = c.String(nullable: false, maxLength: 40),
                        City = c.String(nullable: false, maxLength: 40),
                        StreetName = c.String(nullable: false, maxLength: 40),
                        StreetNumber = c.Byte(nullable: false),
                        Building = c.String(maxLength: 2),
                        Staircase = c.String(maxLength: 2),
                        ApartmentNr = c.Byte()
                    })
                .PrimaryKey(t => t.Id);
        }

        public override void Down()
        {
            DropTable("dbo.Addresses");
        }
    }
}