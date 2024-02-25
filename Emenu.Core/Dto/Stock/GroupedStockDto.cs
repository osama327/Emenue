using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emenu.Core.Dto.Stock
{
    public class GroupedStockDto
    {
        public int ProductId { get; set; }
        public int VariantId { get; set; }
        public decimal SumQty { get; set; }
        public string? ProductName { get; set; }
        public string? VariantName { get; set; }
        public string? ProductAttributeName { get; set; }
    }
}
