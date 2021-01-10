using System.Data.Entity.Migrations;

namespace WeShop.Migrations
{
    public partial class PopulatePaymentTypes : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO PaymentTypes (Id, Name) VALUES (1,'Cash')");
            Sql("INSERT INTO PaymentTypes (Id, Name) VALUES (2,'InAppCreditCard')");
            Sql("INSERT INTO PaymentTypes (Id, Name) VALUES (3,'AtDeliveryCreditCard')");
        }

        public override void Down()
        {
            Sql("DELETE FROM PaymentTypes where Id = 1");
            Sql("DELETE FROM PaymentTypes where Id = 2");
            Sql("DELETE FROM PaymentTypes where Id = 3");
        }
    }
}