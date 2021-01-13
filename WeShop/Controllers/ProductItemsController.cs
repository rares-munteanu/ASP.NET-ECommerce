using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

using Microsoft.AspNet.Identity;

using WeShop.Models;

namespace WeShop.Controllers
{
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

        // GET: ProductItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ProductItem productItem = _context.ProductItems.Find(id);
            if (productItem == null)
            {
                return HttpNotFound();
            }

            return View(productItem);
        }

        // GET: ProductItems/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(Include = "Id,IdOfRelatedProduct,OrderId,Name,Description,Price,ImagePath,Quantity")]
            ProductItem productItem)
        {
            if (ModelState.IsValid)
            {
                _context.ProductItems.Add(productItem);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(productItem);
        }

        // GET: ProductItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ProductItem productItem = _context.ProductItems.Find(id);
            if (productItem == null)
            {
                return HttpNotFound();
            }

            return View(productItem);
        }

        // POST: ProductItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(Include = "Id,IdOfRelatedProduct,OrderId,Name,Description,Price,ImagePath,Quantity")]
            ProductItem productItem)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(productItem).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(productItem);
        }

        // GET: ProductItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ProductItem productItem = _context.ProductItems.Find(id);
            if (productItem == null)
            {
                return HttpNotFound();
            }

            return View(productItem);
        }

        // POST: ProductItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductItem productItem = _context.ProductItems.Find(id);
            _context.ProductItems.Remove(productItem);
            _context.SaveChanges();
            return RedirectToAction("Index");
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