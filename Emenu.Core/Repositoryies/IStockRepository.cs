using Emenu.Core.Dto.Stock;
using Emenu.Core.Interfaces;
using Emenu.Core.Model;
using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emenu.Core.Repositoryies
{
    public interface IStockRepository : IBaseRepository<Stock>
    {
       public Task<ErrorOr<IEnumerable<GroupedStockDto>>>  AllProductsGrouping();
       public Task<ErrorOr<GroupedStockDto>>  ProductGrouping(int id);
    }
}
