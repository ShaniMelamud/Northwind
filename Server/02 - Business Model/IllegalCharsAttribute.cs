using System.ComponentModel.DataAnnotations;

namespace Cool
{
    public class IllegalCharsAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            if (value.ToString().Contains("--"))
                return new ValidationResult("-- is illegal");

            if (value.ToString().Contains(";"))
                return new ValidationResult("; is illegal");

            return ValidationResult.Success;
        }
    }
}
