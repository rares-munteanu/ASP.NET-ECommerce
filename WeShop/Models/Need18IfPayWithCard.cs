using System;
using System.ComponentModel.DataAnnotations;

namespace WeShop.Models
{
    public class Need18IfPayWithCard : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var Order = (Order) validationContext.ObjectInstance;

            if (Order.PaymentTypeId == PaymentType.Unknown ||
                Order.PaymentTypeId == PaymentType.Cash)
                return ValidationResult.Success;

            if (Order.User.Profile == null)
                return new ValidationResult("Profile is required");

            var age = DateTime.Today.Year - Order.User.Profile.Birthdate.Year;

            return age > 18
                ? ValidationResult.Success
                : new ValidationResult("User should be at least 18 years old to go pay with credit card");
        }
    }
}