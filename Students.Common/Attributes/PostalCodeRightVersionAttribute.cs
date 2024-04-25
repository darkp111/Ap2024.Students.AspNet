using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Students.Common.Attributes
{
    public class PostalCodeRightVersionAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is string str)
            {
                
                if (Regex.IsMatch(str, @"\d{2}-\d{3}$"))
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("The field must start with 2 digits then '-' and then 3 digits more.");
                }
            }

            
            return new ValidationResult("Invalid input.");
        }
    }
}
