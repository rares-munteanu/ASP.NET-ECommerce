using System.ComponentModel.DataAnnotations;

namespace WeShop.Models
{
    public class PaymentType
    {
        public byte Id { get; set; }

        [Required]
        public string Name { get; set; }

        public static readonly byte Unknown = 0;
        public static readonly byte Cash = 1;
        public static readonly byte InAppCreditCard = 2;
        public static readonly byte AtDeliveryCreditCard = 3;
    }
}