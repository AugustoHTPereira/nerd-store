using Microsoft.EntityFrameworkCore;
using NSE.Catalog.API.Extensions;
using NSE.Catalog.API.Models;
using NSE.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NSE.Catalog.API.Data.Context
{
    public class CatalogDbContext : DbContext, IUnitOfWork
    {
        public CatalogDbContext(DbContextOptions<CatalogDbContext> options)
            : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }

        public async Task<bool> Commit()
        {
            return await base.SaveChangesAsync() > 0; 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyDefaultColumnTypes();
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogDbContext).Assembly);
        }

        internal object AsNoTracking()
        {
            throw new NotImplementedException();
        }
    }
}
