using Emenu.Core.Interfaces;
using Emenu.Core.Model;
using Emenu.Core.Repositoryies;
using Emenu.DAL.Repositoryies;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using Image = Emenu.Core.Model.Image;

namespace Emenu.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IBaseRepository<Product> Products { get; private set; }
        public IBaseRepository<Variant> Variants { get; private set; }
        public IBaseRepository<ProductAttribute> ProductAttributes { get; private set; }
        public IBaseRepository<Image> Images { get; private set; }
        public IStockRepository Stocks { get; private set; }
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

            Products = new BaseRepository<Product>(context);
            Variants = new BaseRepository<Variant>(context);
            ProductAttributes = new BaseRepository<ProductAttribute>(context);
            Images = new BaseRepository<Image>(context);
            Stocks = new StockRepository(context);
        }

        public async Task<int> SaveAsync()
        {
            int result = await _context.SaveChangesAsync();
            return result;
        }
        public int Save()
        {
            return _context.SaveChanges();
        }

        public async Task DisposeAsync()
            => await _context.DisposeAsync();

        public void Dispose()
            => _context.Dispose();

    }
}
