using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

using WeShop.Models;

namespace WeShop.Controllers.Api
{
    public class ProductsController : ApiController
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        // GET: api/Products
        public IQueryable<Product> GetProducts()
        {
            return _context.Products.Where(p => p.NumberInStock > 0);
        }

        // GET: api/Products/5
        [ResponseType(typeof(Product))]
        public IHttpActionResult GetProduct(int id)
        {
            Product product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }

            base.Dispose(disposing);
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Count(e => e.Id == id) > 0;
        }
    }
}