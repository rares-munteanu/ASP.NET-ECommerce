using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WeShop.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        public Address DeliveryAddress { get; set; }

        public PaymentType PaymentType { get; set; }
        public byte PaymentTypeId { get; set; }

        [Required]
        public ICollection<Product> Products { get; set; }
    }
}