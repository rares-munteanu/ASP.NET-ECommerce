using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WeShop.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public string Description { get; set; }

        public int Price { get; set; }

        [Required]
        public string ImagePath { get; set; }

        public int NumberInStock { get; set; }

        public ICollection<Item> Items { get; set; } //one-to-many cu Item
    }
}