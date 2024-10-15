using System.ComponentModel.DataAnnotations;

namespace AdmissionCommittee.Domain.Attributes;

/// <summary>
/// An attribute to validate birthdate
/// </summary>
public class BirthdateValidationAttribute : ValidationAttribute
{
    /// <summary>
    /// Validated birthdate
    /// </summary>
    /// <param name="value"></param>
    /// <param name="validationContext"></param>
    /// <returns>validation result</returns>
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (value is DateTime birthday)
        {
            if (birthday > DateTime.Now)
            {
                return new ValidationResult("Birthday cannot be in the future.");
            }
        }
        return ValidationResult.Success!;
    }
}


