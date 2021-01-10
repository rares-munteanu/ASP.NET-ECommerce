﻿using System;
using System.ComponentModel.DataAnnotations;

namespace WeShop.Models
{
    public class Profile
    {
        public int Id { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "First name should be between 3 and 40 characters")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }


        [Required]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "Last name should be between 3 and 40 characters")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }


        [Required]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Phone number should be between 10 and 15 digits")]
        [RegularExpression(@"^0\d{9}|0\d{3}-\d{3}-\d{3}",
            ErrorMessage = "Not a valid phone number")]
        public string Phone { get; set; }


        [Display(Name = "Date of Birth")]
        public DateTime Birthdate { get; set; }
    }
}