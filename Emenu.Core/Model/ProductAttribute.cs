using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emenu.Core.Model
{
    public class ProductAttribute
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(250)]
        public string? Name { get; set; } = string.Empty;
    }
}
