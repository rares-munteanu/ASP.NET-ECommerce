using System.Linq;
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

            var relatedProduct = _context.Products.Single(p => p.Id == newProductItem.IdOfRelatedProduct);

            var productsItemInDb = _context.ProductItems
                .Where(pi =>
                    pi.OrderId == newProductItem.OrderId && pi.IdOfRelatedProduct == newProductItem.IdOfRelatedProduct)
                .ToList();

            if (productsItemInDb.Count == 0)
            {
                _context.ProductItems.Add(newProductItem);
                relatedProduct.NumberInStock--;
                _context.SaveChanges();
                return CreatedAtRoute("DefaultApi", new {id = newProductItem.Id}, newProductItem);
            }

            foreach (var productItemInDb in productsItemInDb)
            {
                if (productItemDto.Adding)
                {
                    productItemInDb.Quantity++;
                    relatedProduct.NumberInStock--;
                }
                else if (productItemInDb.Quantity == 1)
                {
                    _context.ProductItems.Remove(productItemInDb);
                    relatedProduct.NumberInStock++;
                }
                else if (productItemInDb.Quantity > 0)
                {
                    productItemInDb.Quantity--;
                    relatedProduct.NumberInStock++;
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