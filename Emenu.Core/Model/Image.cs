using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emenu.Core.Model
{
    public class Image
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(500)]
        public string Photo { get; set; } = string.Empty;

        public bool IsBasec { get; set; }

        public int productId { get; set; }

        public Product Product { get; set; } = default!;
    }
}
