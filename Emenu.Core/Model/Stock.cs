using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emenu.Core.Model
{
    public class Stock
    {
        [Key]
        public int Id { get; set; }

        public int? VariantId { get; set; }

        public Variant? Variant { get; set; } = default!;
        public int? ProductAttributeId { get; set; }
        public ProductAttribute ProductAttribute { get; set; } = default!;
        public int? ProductId { get; set; }
        public Product Product { get; set; } = default!;
        public decimal? Qty { get; set; }
    }
}
