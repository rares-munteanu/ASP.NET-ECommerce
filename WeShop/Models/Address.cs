using System.ComponentModel.DataAnnotations;

namespace WeShop.Models
{
    public class Address
    {
        public int Id { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 4, ErrorMessage = "County should be between 4 and 40 characters")]
        public string County { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 4, ErrorMessage = "City should be between 4 and 40 characters")]
        [Display(Name = "City (or village)")]
        public string City { get; set; }

        [Required]
        [StringLength(40)]
        [Display(Name = "Street name")]
        public string StreetName { get; set; }

        [Display(Name = "Street number")]
        public byte StreetNumber { get; set; }

        [StringLength(2, MinimumLength = 1)]
        [Display(Name = "Building (if case)")]
        public string Building { get; set; }

        [StringLength(2, MinimumLength = 1)]
        [Display(Name = "Staircase (if case)")]
        public string Staircase { get; set; }

        [Display(Name = "Apartment number (if case)")]
        public byte? ApartmentNr { get; set; }
    }
}