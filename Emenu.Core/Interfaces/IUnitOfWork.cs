using Emenu.Core.Model;
using Emenu.Core.Repositoryies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emenu.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable  
    {
        public IBaseRepository<Product> Products { get; }
        public IBaseRepository<Variant> Variants { get; }
        public IBaseRepository<ProductAttribute> ProductAttributes { get; }
        public IBaseRepository<Image> Images { get; }
        public IStockRepository Stocks { get; }

        Task<int> SaveAsync();
        int Save();
        Task DisposeAsync();
        void Dispose();
    }
}
