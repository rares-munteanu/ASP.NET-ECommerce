using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeShop.Models
{
    public class Profile
    {
        [Key]
        [ForeignKey("User")]
        public string Id { get; set; }

        public virtual User User { get; set; }

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


        [Display(Name = "Date of Birth")]
        public DateTime Birthdate { get; set; }
    }
}