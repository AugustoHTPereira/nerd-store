using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using NSE.Auth.API.Configuration.EntityFramework;
using NSE.Auth.API.Configuration.Identity;
using NSE.Auth.API.Configuration.Jwt;
using NSE.Core.Services.Identity;

namespace NSE.Auth.API
{
    public class Startup
    {
        public Startup(IHostEnvironment hostEnvironment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(hostEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{hostEnvironment.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddEntityFramework(Configuration.GetConnectionString("DefaultConnection"));
            services.AddIdentity();
            services.AddJwt(Configuration.GetSection("JwtOptions").Get<TokenOptions>());

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Nerd Store Identity",
                    Version = "v1",
                    Description = "Authentication Store API",
                    Contact = new OpenApiContact { Email = "augustohtp8@gmail.com", Name = "Augusto Pereira" }
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "NSE.Auth.API v1"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthSupport();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
