using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeShop.Models
{
    public class Order
    {
        public int Id { get; set; }

        [ForeignKey("UserId")]

        public User User { get; set; }

        [Required]
        public string UserId { get; set; }


        [ForeignKey("PaymentTypeId")]
        [Display(Name = "Payment type")]
        public PaymentType PaymentType { get; set; }

        [Required]
        //[Need18IfPayWithCard]
        public byte PaymentTypeId { get; set; }

        [Required]
        public string OrderStatus { get; set; }

        public ICollection<ProductItem> ProductItems { get; set; }
    }
}