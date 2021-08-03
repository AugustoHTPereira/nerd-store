using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using NSE.Web.MVC.Models.Base;
using NSE.Web.MVC.Models.User;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NSE.Web.MVC.Services
{
    public class AuthService : ServiceBase, IAuthService
    {
        private readonly HttpClient Client;

        public AuthService(HttpClient client)
        {
            Client = client;
        }

        public async Task<APIResponseBase> LoginAsync(HttpContext context, LoginViewModel request)
        {
            var response = await HandleHttpResponseAsync<LoginModelResponse>(await Client.PostAsJsonAsync("/api/auth/login", request));
            if (!response.IsValid)
                return response;

            var securityToken = new JwtSecurityTokenHandler().ReadJwtToken(response.Data.Token.Bearer);
            var claims = new List<Claim>();
            claims.Add(new Claim("JWT/Bearer", response.Data.Token.Bearer));
            claims.Add(new Claim("Refresh/Bearer", response.Data.Token.RefreshToken));
            claims.AddRange(securityToken.Claims);

            foreach (var claim in response.Data.Token.Claims)
                if (!claims.Any(x => x.Type == claim.Type))
                    claims.Add(new Claim(claim.Type, claim.Value));

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTime.Parse(securityToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Expiration).Value)
            };

            await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme)), authProperties);
            return response;
        }

        public async Task LogoutAsync(HttpContext context)
        {
            await context.SignOutAsync();
        }

        public async Task<APIResponseBase> RegisterAsync(RegisterViewModel request)
        {
            return await HandleHttpResponseAsync(await Client.PostAsJsonAsync("/api/auth/register", request));
        }
    }
}
