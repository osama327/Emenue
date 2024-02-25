using Emenu.Core.Dto.Stock;
using Emenu.Core.Model;
using Emenu.Core.Repositoryies;
using ErrorOr;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emenu.DAL.Repositoryies
{
    public class StockRepository : BaseRepository<Stock>, IStockRepository
    {
        private readonly ApplicationDbContext _context;

        public StockRepository(ApplicationDbContext context) : base(context)
        {
                _context = context;
        }
        public async Task<ErrorOr<IEnumerable<GroupedStockDto>>> AllProductsGrouping()
        {
            try
            {
                var res = await _context.Stocks.Include("Product").Include("Variant")
                                               .GroupBy(x => new {x.ProductId , x.VariantId})
                                               .Select(z => new GroupedStockDto
                                               {
                                                   ProductId = z.Key.ProductId.Value,
                                                   VariantId = z.Key.VariantId.Value,
                                                   SumQty = z.Sum(x => x.Qty.Value),
                                                   ProductName = z.FirstOrDefault().Product.Name,
                                                   VariantName = z.FirstOrDefault().Variant.Name ,
                                                   ProductAttributeName = z.FirstOrDefault().ProductAttribute.Name,

                                               }).ToListAsync();
                if (res == null)
                {
                    return Error.Failure(description: "Not Found Data");
                }
                return res;
            }
            catch (Exception ex)
            {
                return Error.Failure(description: ex.Message);
            }
        }

        public async Task<ErrorOr<GroupedStockDto>> ProductGrouping(int id)
        {
            try
            {
                var res = await _context.Stocks.Include("Product").Include("Variant").Where(p => p.ProductId == id)
                                               .GroupBy(x => new { x.VariantId })
                                               .Select(z => new GroupedStockDto
                                               {
                                                   ProductId = id,
                                                   VariantId = z.Key.VariantId.Value,
                                                   SumQty = z.Sum(x => x.Qty.Value),
                                                   ProductName = z.FirstOrDefault().Product.Name,
                                                   VariantName = z.FirstOrDefault().Variant.Name,
                                                   ProductAttributeName = z.FirstOrDefault().ProductAttribute.Name,

                                               }).FirstOrDefaultAsync();
                if (res == null)
                {
                    return Error.Failure(description: "Not Found Data");
                }
                return res;
            }
            catch (Exception ex)
            {
                return Error.Failure(description: ex.Message);
            }
        }
    }
}
