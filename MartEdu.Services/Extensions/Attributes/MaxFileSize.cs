using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace MartEdu.Service.Extensions.Attributes
{
    public class MaxFileSize : ValidationAttribute
    {

        private readonly int _maxFileSize;
        public MaxFileSize(int maxFileSize)
        {
            _maxFileSize = maxFileSize * 1024 * 1024;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                if (file.Length > _maxFileSize)
                {
                    return new ValidationResult(GetErrorMessage());
                }
            }
            return ValidationResult.Success;
        }

        public string GetErrorMessage()
        {
            return $"Maximum allowed file size is {_maxFileSize} bytes.";
        }
    }
}
