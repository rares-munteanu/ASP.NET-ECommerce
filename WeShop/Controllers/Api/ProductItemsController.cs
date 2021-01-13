using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

using WeShop.Dtos;
using WeShop.Models;

namespace WeShop.Controllers.Api
{
    public class ProductItemsController : ApiController
    {
        private ApplicationDbContext _context = new ApplicationDbContext();


        // -----------

        // POST: api/ProductItems
        [ResponseType(typeof(ProductItem))]
        public IHttpActionResult CreateOrUpdateProductItem(ProductItemDto productItemDto)
        {
            var newProductItem = new ProductItem(productItemDto);

            var productsItemInDb = _context.ProductItems
                .Where(pi =>
                    pi.OrderId == newProductItem.OrderId && pi.IdOfRelatedProduct == newProductItem.IdOfRelatedProduct)
                .ToList();

            if (productsItemInDb.Count == 0)
            {
                _context.ProductItems.Add(newProductItem);
                _context.SaveChanges();
                return CreatedAtRoute("DefaultApi", new {id = newProductItem.Id}, newProductItem);
            }

            foreach (var productItemInDb in productsItemInDb)
            {
                if (productItemDto.Adding)
                    productItemInDb.Quantity++;
                else if (productItemInDb.Quantity == 1)
                {
                    _context.ProductItems.Remove(productItemInDb);
                }
                else if (productItemInDb.Quantity > 0)
                {
                    productItemInDb.Quantity--;
                }

                _context.SaveChanges();
            }

            return Ok();
        }


        // GET: api/ProductItems
        [HttpGet]
        public IQueryable<ProductItem> GetProductItems(int? activeOrderId = null)
        {
            if (activeOrderId != null)
                return _context.ProductItems.Where(pi => pi.OrderId == activeOrderId);
            return _context.ProductItems;
        }

        // GET: api/ProductItems/5
        [ResponseType(typeof(ProductItem))]
        public IHttpActionResult GetProductItem(int id)
        {
            ProductItem productItem = _context.ProductItems.Find(id);
            if (productItem == null)
            {
                return NotFound();
            }

            return Ok(productItem);
        }

        // PUT: api/ProductItems/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProductItem(int id, ProductItem productItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != productItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(productItem).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        //// POST: api/ProductItems
        //[ResponseType(typeof(ProductItem))]
        //public IHttpActionResult PostProductItem(ProductItem productItem)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    _context.ProductItems.Add(productItem);
        //    _context.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new {id = productItem.Id}, productItem);
        //}

        // DELETE: api/ProductItems/5
        [ResponseType(typeof(ProductItem))]
        public IHttpActionResult DeleteProductItem(int id)
        {
            ProductItem productItem = _context.ProductItems.Find(id);
            if (productItem == null)
            {
                return NotFound();
            }

            _context.ProductItems.Remove(productItem);
            _context.SaveChanges();

            return Ok(productItem);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }

            base.Dispose(disposing);
        }

        private bool ProductItemExists(int id)
        {
            return _context.ProductItems.Count(e => e.Id == id) > 0;
        }
    }
}