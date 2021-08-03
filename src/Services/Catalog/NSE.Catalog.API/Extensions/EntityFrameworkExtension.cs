using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NSE.Catalog.API.Data.Context;

namespace NSE.Catalog.API.Extensions
{
    public static class EntityFrameworkExtension
    {
        public static void AddCatalogContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<CatalogDbContext>(x => x.UseSqlServer(connectionString));
        }
    }
}
