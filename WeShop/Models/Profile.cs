using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WeShop.Models
{
    public class Profile
    {
        public int Id { get; set; }

        [Required]
        public User User { get; set; } // One-to-one cu User

        public ICollection<Address> Address { get; set; }

        [Required]
        [StringLength(40)]
        public string FirstName { get; set; }


        [Required]
        [StringLength(40)]
        public string LastName { get; set; }


        [Required]
        [StringLength(10)]
        public string Phone { get; set; }

        public DateTime Birthdate { get; set; }
    }
}