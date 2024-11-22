using System.ComponentModel.DataAnnotations;

namespace ApiApplication1.ValidationAttributes
{
    public class WeatherDateValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is not DateOnly date)
            {
                return new ValidationResult(ErrorMessage ?? "La date n'est pas au bon format");
            }

            if (date < DateOnly.FromDateTime(DateTime.Now) || date > DateOnly.FromDateTime(DateTime.Now.AddDays(5)))
            {
                return new ValidationResult(ErrorMessage ?? "La date ne peut pas dépasser 5 jours à partir d'aujourd'hui");
            }

            return ValidationResult.Success;
        }
    }
}
