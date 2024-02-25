using Emenu.Core.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emenu.Core.Dto.Product
{
    public class ProductCreateDto
    {
        public int? Id { get; set; }
        public string? Name { get; set; } 


        public string? Description { get; set; } 


        public decimal? Price { get; set; }

        //public List<IFormFile> Images { get; set; } = default!;

    }
}
