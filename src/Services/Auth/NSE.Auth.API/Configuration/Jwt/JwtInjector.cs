using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace NSE.Auth.API.Configuration.Jwt
{
    public static class JwtInjector
    {
        public static void AddJwt(this IServiceCollection services, JwtOptions jwtOptions)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (jwtOptions == null) throw new ArgumentNullException(nameof(jwtOptions));

            services.AddSingleton(jwtOptions);
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = true;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtOptions.Key)),
                    ValidateAudience = true,
                    ValidAudience = jwtOptions.Audience,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidateLifetime = true,
                };
            });

            services.AddScoped<IJwtService, JwtService>();
        }
    }
}
