using Microsoft.Extensions.DependencyInjection;
using NSE.Core.Services.Identity;
using System;

namespace NSE.Auth.API.Configuration.Jwt
{
    public static class JwtInjector
    {
        public static void AddJwt(this IServiceCollection services, TokenOptions options)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (options == null) throw new ArgumentNullException(nameof(options));

            services.AddAuthSupport(options);
            services.AddSingleton(options);
            services.AddScoped<IJwtService, JwtService>();
        }
    }
}
