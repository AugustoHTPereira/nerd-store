using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSE.Web.MVC.Configuration.Sections;
using NSE.Web.MVC.Services;
using System;

namespace NSE.Web.MVC.Configuration
{
    public static class ServicesInjector
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            var apiSection = configuration.GetSection("API").Get<APISection>();
            services.AddHttpClient<IAuthService, AuthService>(x =>
            {
                x.BaseAddress = new Uri(apiSection.BaseAddress);
            });
        }
    }
}
