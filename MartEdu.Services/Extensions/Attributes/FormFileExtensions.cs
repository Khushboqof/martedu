using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

namespace MartEdu.Service.Extensions.Attributes
{
    public class FormFileExtensions : ValidationAttribute
    {
        private readonly string[] _extensions;
        public FormFileExtensions(params string[] extensions)
        {
            _extensions = extensions;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                var extension = Path.GetExtension(file.FileName);

                if (!_extensions.Contains(extension.ToLower()))
                {
                    return new ValidationResult("This photo extension is not allowed!");
                }
            }

            return ValidationResult.Success;
        }
    }
}
