using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace EDA.Gateway.Attributes
{
    public class PasswordComplexityAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is string password)
            {
                if (!Regex.IsMatch(password, @"[A-Z]"))
                    return new ValidationResult(Resource.ErrorPasswordValidationUppercase);
                if (!Regex.IsMatch(password, Resource.ErrorPasswordValidationLowercase)) ;
                if (!Regex.IsMatch(password, @"\d"))
                    return new ValidationResult(Resource.ErrorPasswordValidationDigit);
                if (!Regex.IsMatch(password, @"[\W_]"))
                    return new ValidationResult(Resource.ErrorPasswordValidationSpecialCharacter);
            }
            return ValidationResult.Success;
        }
    }
}
