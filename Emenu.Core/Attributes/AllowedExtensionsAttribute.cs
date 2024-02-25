using Emenu.Core.Dto.Product;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emenu.Core.Attributes
{
    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string _allowedExtensions;
        public AllowedExtensionsAttribute(string allowedExtensions)
        {
            _allowedExtensions = allowedExtensions;
        }

        // Custom Validate code Attributes []
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var File = value as IFormFile;

            if (File is not null)
            {
                var Extensions = Path.GetFileName(File.FileName);

                var isAllowd = _allowedExtensions.Split(',').Contains(Extensions, StringComparer.OrdinalIgnoreCase);

                if (!isAllowd)
                {
                    return new ValidationResult($"Only {_allowedExtensions}");
                }
            }

            return ValidationResult.Success;
        }
    }
}
