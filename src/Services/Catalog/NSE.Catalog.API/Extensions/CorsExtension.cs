using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NSE.Catalog.API.Extensions
{
    public static class CorsExtension
    {
        public static void AddApiCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("Default", x =>
                    x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()
                );
            });
        }

        public static void UseApiCors(this IApplicationBuilder app)
        {
            app.UseCors("Default");
        }
    }
}
