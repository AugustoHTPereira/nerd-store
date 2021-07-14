using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NSE.Auth.API.Data;
using System;

namespace NSE.Auth.API.Configuration.EntityFramework
{
    public static class EntityFrameworkInjector
    {
        public static void AddEntityFramework(this IServiceCollection services, string connectionString)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException(nameof(connectionString));

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
        }
    }
}
