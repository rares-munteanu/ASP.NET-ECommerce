using System.ComponentModel.DataAnnotations;

namespace WeShop.Dtos
{
    public class ProductItemDto
    {
        public int ProductId { get; set; }

        public int ActiveOrderId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Price { get; set; }

        [Required]
        public string ImagePath { get; set; }

        public bool Adding { get; set; } = true;
    }
}