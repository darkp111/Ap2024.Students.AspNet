using Xunit;
using System.ComponentModel.DataAnnotations;
using Students.Common.Attributes;

public class ValidationTests
{
    [Fact]
    public void FullNameValidation_ValidFullName_ReturnsSuccess()
    {
        // Arrange
        var validationAttribute = new CapitalLettersInNameAndSurname();
        var validationContext = new ValidationContext(new { FullName = "John Doe" });

        // Act
        var validationResult = validationAttribute.GetValidationResult("John Doe", validationContext);

        // Assert
        Assert.Equal(ValidationResult.Success, validationResult);
    }

    [Fact]
    public void FullNameValidation_InvalidFullName_ReturnsError()
    {
        // Arrange
        var validationAttribute = new CapitalLettersInNameAndSurname();
        var validationContext = new ValidationContext(new { FullName = "john doe" });

        // Act
        var validationResult = validationAttribute.GetValidationResult("john doe", validationContext);

        // Assert
        Assert.Equal("First and last name should begin with a capital letter and have only one space", validationResult.ErrorMessage);
    }

    [Fact]
    public void CapitalLettersValidation_ValidInput_ReturnsSuccess()
    {
        // Arrange
        var validationAttribute = new CapitalLettersOnlyAndNoNumbers();
        var validationContext = new ValidationContext(new { FieldValue = "Hello" });

        // Act
        var validationResult = validationAttribute.GetValidationResult("Hello", validationContext);

        // Assert
        Assert.Equal(ValidationResult.Success, validationResult);
    }
}
