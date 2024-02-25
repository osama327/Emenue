using Emenu.Core.Attributes;
using Emenu.Core.Dto.Product;
using Emenu.Core.Settings;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emenu.Core.Dto.Image
{
    public class ImageDto
    {
        public int? Id { get; set; }

       // [AllowedExtensions(FileSetting.AllowedExtensions)]
        public IFormFile? Photo { get; set; } = default!;

        public bool? IsBasec { get; set; }

        public int? productId { get; set; }
    }
}
