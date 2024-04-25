using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Students.Common.Attributes;

public class CapitalLettersOnlyAndNoNumbers : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is string str)
        {
            // Проверка, начинается ли строка с заглавной буквы и не содержит цифры
            if (Regex.IsMatch(str, @"[^A-Z][a-z][^0-9]+$"))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("The field must start with an uppercase letter and should not contain numbers.");
            }
        }

        // Возвращаем ошибку, если входные данные не являются строкой
        return new ValidationResult("Invalid input.");
    }

}
