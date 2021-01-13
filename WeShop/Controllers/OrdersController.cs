using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

using Microsoft.AspNet.Identity;

using WeShop.Models;

namespace WeShop.Controllers
{
    public class OrdersController : Controller
    {
        private ApplicationDbContext _context = new ApplicationDbContext();


        public ActionResult ConfirmOrder()
        {
            var userID = User.Identity.GetUserId();

            var profile = _context.Users.Include("Profile").Single(u => u.Id == userID).Profile;

            if (profile == null)
            {
                return View("ProfileNotFilled");
            }


            var activeUserOrder =
                _context.Orders.Include("ProductItems")
                    .Where(o => o.UserId == userID && o.OrderStatus == OrderStatus.NewOrder).ToList()[0];

            if (activeUserOrder.ProductItems.Count == 0)
            {
                return RedirectToAction("EmptyShoppingCartAcction", "ProductItems");
            }

            activeUserOrder.OrderStatus = OrderStatus.Confirmed;

            _context.SaveChanges();

            return RedirectToAction("Index", "Orders");
        }

        public ActionResult OrderItems(int orderId)
        {
            //var userID = User.Identity.GetUserId();

            var userOrder =
                _context.Orders.Include("ProductItems").SingleOrDefault(uo => uo.Id == orderId);

            if (userOrder.OrderStatus == OrderStatus.NewOrder)
                return View("OrderNotConfirmed");

            if (userOrder == null)
            {
                return HttpNotFound();
            }

            var orderItems = userOrder.ProductItems;

            return View(orderItems);
        }

        // GET: Orders
        public ActionResult Index()
        {
            var userID = User.Identity.GetUserId();
            var orders = _context.Orders.Include(o => o.PaymentType).Include(o => o.User)
                .Where(o => o.UserId == userID);
            return View(orders.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Order order = _context.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            ViewBag.PaymentTypeId = new SelectList(_context.PaymentTypes, "Id", "Name");
            ViewBag.UserId = new SelectList(_context.Users, "Id", "Email");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[Bind(Include = "Id,UserId,PaymentTypeId,OrderStatus")]
        public ActionResult Create(Order order)
        {
            var userID = User.Identity.GetUserId();
            var user = _context.Users.Include("Orders").Single(u => u.Id == userID);

            var userOrders = _context.Orders.Where(o => o.UserId == userID && o.OrderStatus == OrderStatus.NewOrder)
                .ToList();

            //var userOrders = user.Orders.Where(o => o.OrderStatus == OrderStatus.NewOrder);

            if (userOrders.Count != 0)
            {
                return View("NewOrderExists");
            }

            order.User = user;
            order.UserId = userID;

            order.OrderStatus = OrderStatus.NewOrder;

            ModelState.Clear();
            TryUpdateModel(order);

            if (ModelState.IsValid)
            {
                _context.Orders.Add(order);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PaymentTypeId = new SelectList(_context.PaymentTypes, "Id", "Name", order.PaymentTypeId);
            ViewBag.UserId = new SelectList(_context.Users, "Id", "Email", order.UserId);
            return View(order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Order order = _context.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }

            ViewBag.PaymentTypeId = new SelectList(_context.PaymentTypes, "Id", "Name", order.PaymentTypeId);
            ViewBag.UserId = new SelectList(_context.Users, "Id", "Email", order.UserId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,PaymentTypeId,OrderStatus")]
            Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(order).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PaymentTypeId = new SelectList(_context.PaymentTypes, "Id", "Name", order.PaymentTypeId);
            ViewBag.UserId = new SelectList(_context.Users, "Id", "Email", order.UserId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Order order = _context.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }


            var relatedProductsItem = _context.ProductItems.Where(pi => pi.OrderId == order.Id);
            foreach (var relatedProductItem in relatedProductsItem)
            {
                _context.ProductItems.Remove(relatedProductItem);
            }

            _context.Orders.Remove(order);
            _context.SaveChanges();
            return RedirectToAction("Index");

            //return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = _context.Orders.Find(id);
            _context.Orders.Remove(order);
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