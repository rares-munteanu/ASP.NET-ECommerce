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

        public PaymentType PaymentType { get; set; }
        public byte PaymentTypeId { get; set; }

        //public ICollection<ProductItem> ProductItems { get; set; }
    }
}