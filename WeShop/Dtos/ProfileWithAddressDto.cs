using WeShop.Models;

namespace WeShop.Dtos
{
    public class ProfileWithAddressDto
    {
        public Profile Profile { get; set; }

        public Address Address { get; set; }
    }
}