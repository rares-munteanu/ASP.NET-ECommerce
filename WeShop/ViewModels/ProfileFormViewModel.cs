using System;
using System.ComponentModel.DataAnnotations;

using WeShop.Models;

namespace WeShop.ViewModels
{
    public class ProfileFormViewModel
    {
        public int? Id { get; set; }

        public Address Address { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "First name should be between 3 and 40 characters")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }


        [Required]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "Last name should be between 3 and 40 characters")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }


        [Required]
        [StringLength(10, MinimumLength = 10)]
        [RegularExpression(@"^0\d{9}|0\d{3}-\d{3}-\d{3}",
            ErrorMessage = "Not a valid phone number")]
        public string Phone { get; set; }

        [Required]
        [Display(Name = "Date of Birth")]
        public DateTime? Birthdate { get; set; }


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

        [Required]
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


        public string Title => Id == 0 ? "You don't have a profile. Please configure one." : "Edit your profile";

        public ProfileFormViewModel()
        {
            Id = 0;
        }

        public ProfileFormViewModel(Profile profile, Address address)
        {
            Id = profile.Id;
            FirstName = profile.FirstName;
            LastName = profile.LastName;
            Phone = profile.Phone;
            Birthdate = profile.Birthdate;
            County = address.County;
            City = address.City;
            StreetName = address.StreetName;
            StreetNumber = address.StreetNumber;
            Building = address.Building;
            Staircase = address.Staircase;
            ApartmentNr = address.ApartmentNr;

            //Address = profile.Address;
        }
    }
}