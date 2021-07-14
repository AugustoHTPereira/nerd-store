using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using NSE.Auth.API.Data;
using System;

namespace NSE.Auth.API.Configuration.Identity
{
    public static class IdentityInjector
    {
        public static void AddIdentity(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
        }
    }
}
