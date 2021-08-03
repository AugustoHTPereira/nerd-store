using Microsoft.Extensions.DependencyInjection;
using NSE.Catalog.API.Data.Context;
using NSE.Catalog.API.Data.Repositories;
using NSE.Catalog.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NSE.Catalog.API.Extensions
{
    public static class ServicesExtension
    {
        public static void AddCatalogServices(this IServiceCollection services)
        {
            services.AddScoped<CatalogDbContext>();
            services.AddScoped<IProductRepository, ProductRepository>();
        }
    }
}
