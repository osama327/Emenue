using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emenu.Core.Dto.Stock
{
    public class StockDto
    {
        public int? Id { get; set; }

        public int? VariantId { get; set; }

        public int? ProductAttributeId { get; set; }
        public int? ProductId { get; set; }
        public int? Qty { get; set; }
    }
}
