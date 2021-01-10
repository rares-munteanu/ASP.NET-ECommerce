using System.Data.Entity;

using Microsoft.AspNet.Identity.EntityFramework;

namespace WeShop.Models
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<Order> Orders { get; set; }

        public ApplicationDbContext()
            : base("WeShopConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}