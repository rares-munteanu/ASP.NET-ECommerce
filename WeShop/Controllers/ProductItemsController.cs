using System.Linq;
using System.Web.Mvc;

using Microsoft.AspNet.Identity;

using WeShop.Models;

namespace WeShop.Controllers
{
    [Authorize]
    public class ProductItemsController : Controller
    {
        private ApplicationDbContext _context = new ApplicationDbContext();

        // GET: ProductItems
        public ActionResult Index()
        {
            var userID = User.Identity.GetUserId();

            var activeUserOrder =
                _context.Orders.Where(o => o.UserId == userID && o.OrderStatus == OrderStatus.NewOrder).ToList();

            if (activeUserOrder.Count == 0)
                return RedirectToAction("NewOrderNotFoundAction", "Products");

            var activeOrderId = activeUserOrder[0].Id;
            var productItemsInShoppingCart =
                _context.ProductItems.Where(pi => pi.OrderId == activeOrderId).ToList();
            if (productItemsInShoppingCart.Count == 0)
                return View("EmptyShoppingCart");

            ViewBag.ActiveOrderId = activeOrderId;
            //return View("ShoppingCart");
            return View("ShoppingCart", _context.ProductItems.Where(pi => pi.OrderId == activeOrderId).ToList());
        }

        public ActionResult EmptyShoppingCartAcction()
        {
            return View("EmptyShoppingCart");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}