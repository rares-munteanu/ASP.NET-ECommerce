using System.Data.Entity.Migrations;

namespace WeShop.Migrations
{
    public partial class AddPaymentType : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                    "dbo.PaymentTypes",
                    c => new
                    {
                        Id = c.Byte(nullable: false),
                        Name = c.String(nullable: false)
                    })
                .PrimaryKey(t => t.Id);
        }

        public override void Down()
        {
            DropTable("dbo.PaymentTypes");
        }
    }
}