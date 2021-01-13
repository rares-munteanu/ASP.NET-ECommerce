using System.ComponentModel.DataAnnotations;

using WeShop.Dtos;

namespace WeShop.Models
{
    public class ProductItem
    {
        [Key]
        public int Id { get; set; }

        public int IdOfRelatedProduct { get; set; }

        public int OrderId { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public string Description { get; set; }

        public int Price { get; set; }

        [Required]
        public string ImagePath { get; set; }

        public int Quantity { get; set; }

        public ProductItem()
        {
        }

        public ProductItem(ProductItemDto productItemDto)
        {
            Name = productItemDto.Name;
            Description = productItemDto.Description;
            Price = productItemDto.Price;
            ImagePath = productItemDto.ImagePath;
            ImagePath = productItemDto.ImagePath;
            IdOfRelatedProduct = productItemDto.ProductId;
            OrderId = productItemDto.ActiveOrderId;
            Quantity = 1;
        }
    }
}