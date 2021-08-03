using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace NSE.Catalog.API.Extensions
{
    public static class SwaggerExtension
    {
        public static void AddSwaggerDocs(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Nerd Store Catalog",
                    Version = "v1",
                    Description = "Catalog API",
                    Contact = new OpenApiContact { Email = "augustohtp8@gmail.com", Name = "Augusto Pereira" }
                });
            });
        }

        public static void UseSwaggerDocs(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "NSE.Catalog.API v1"));
        }
    }
}
