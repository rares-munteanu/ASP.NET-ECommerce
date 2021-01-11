using System.Linq;
using System.Web.Mvc;

using Microsoft.AspNet.Identity;

using WeShop.Models;

namespace WeShop.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProfileController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [HttpGet]
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            //var user = _context.Users.Single(u => u.Id == userId);
            //var userProfile = _context.Profiles.SingleOrDefault(p => p.User == user);
            var userProfile = _context.Profiles
                .Include("User")
                .SingleOrDefault(u => u.User.Id == userId);

            return userProfile == null ? View("ProfileNotFound") : View(userProfile);
        }
    }
}