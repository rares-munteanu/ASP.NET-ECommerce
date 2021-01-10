using System.Data.Entity;

using Microsoft.AspNet.Identity.EntityFramework;

namespace WeShop.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Profile> Profiles { get; set; }

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