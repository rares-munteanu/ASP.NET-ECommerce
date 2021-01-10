using System.Collections.Generic;

namespace WeShop.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }

        public ICollection<Item> Items { get; set; }
    }
}