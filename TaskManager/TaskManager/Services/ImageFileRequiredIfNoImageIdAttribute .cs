using System.ComponentModel.DataAnnotations;
using TaskManager.Models;

namespace TaskManager.Services
{
    public class ImageFileRequiredIfNoImageIdAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return ValidationResult.Success;
        }
    }
}
