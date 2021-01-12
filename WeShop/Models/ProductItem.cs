using System.ComponentModel.DataAnnotations;

namespace WeShop.Models
{
    public class ProductItem
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public string Description { get; set; }

        public int Price { get; set; }

        [Required]
        public string ImagePath { get; set; }

        public int Quantity { get; set; }
    }
}