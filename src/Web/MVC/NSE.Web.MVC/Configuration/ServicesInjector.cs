using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSE.Web.MVC.Configuration.Sections;
using NSE.Web.MVC.Extensions;
using NSE.Web.MVC.Extensions.Middlewares;
using NSE.Web.MVC.Services;
using NSE.Web.MVC.Services.Handlers;
using Polly;
using System;

namespace NSE.Web.MVC.Configuration
{
    public static class ServicesInjector
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            #region Http services

            services.AddTransient<HttpAuthorizeDelegatingHandler>();

            var apiSection = configuration.GetSection("API").Get<APISection>();
            services.AddHttpClient<IAuthService, AuthService>("AuthService", x => x.BaseAddress = new Uri(apiSection.AuthBaseAddress))
                .AddTransientHttpErrorPolicy(x => x.WaitAndRetryAsync(3, _ => TimeSpan.FromSeconds(1)))
                .AddTransientHttpErrorPolicy(x => x.CircuitBreakerAsync(5, TimeSpan.FromSeconds(90)));

            #region Refit

            services.AddHttpClient("CatalogService", x => x.BaseAddress = new Uri(apiSection.CatalogBaseAddress))
                .AddHttpMessageHandler<HttpAuthorizeDelegatingHandler>()
                .AddTypedClient(Refit.RestService.For<ICatalogService>)
                .AddTransientHttpErrorPolicy(x => x.WaitAndRetryAsync(3, _ => TimeSpan.FromSeconds(1)))
                .AddTransientHttpErrorPolicy(x => x.CircuitBreakerAsync(5, TimeSpan.FromSeconds(90)));


            #endregion

            #endregion

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUser, IdentityUser>();
        }

        public static void UseExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
