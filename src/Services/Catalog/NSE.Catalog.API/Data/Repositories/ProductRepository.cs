using Microsoft.EntityFrameworkCore;
using NSE.Catalog.API.Data.Context;
using NSE.Catalog.API.Models;
using NSE.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NSE.Catalog.API.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly CatalogDbContext DbContext;

        public ProductRepository(CatalogDbContext dbContext)
        {
            DbContext = dbContext;
        }

        private IUnitOfWork Uow => DbContext;

        public void Delete(Product model)
        {
            DbContext.Remove(model);
            Uow.Commit();
        }

        public void Delete(Guid id)
        {
            DbContext.Products.Remove(DbContext.Products.Find(id));
            Uow.Commit();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            DbContext?.Dispose();
        }

        public void Insert(Product model)
        {
            DbContext.Products.Add(model);
            Uow.Commit();
        }

        public async Task<IEnumerable<Product>> SelectAsync()
        {
            return await DbContext.Products.AsNoTracking().ToListAsync();
        }

        public async Task<Product> SelectAsync(Guid id)
        {
            return await DbContext.Products.FindAsync(id);
        }

        public void Update(Product model)
        {
            DbContext.Products.Update(model);
            Uow.Commit();
        }
    }
}
