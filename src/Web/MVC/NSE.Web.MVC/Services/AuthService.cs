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
using System.Text.Json;
using System.Threading.Tasks;

namespace NSE.Web.MVC.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient Client;

        public AuthService(HttpClient client)
        {
            Client = client;
        }

        public async Task<APIResponseBase> LoginAsync(HttpContext context, LoginViewModel request)
        {
            var response = await Client.PostAsJsonAsync("/api/auth/login", request);
            var responseContent = JsonSerializer.Deserialize<APIResponseBase<LoginModelResponse>>(await response.Content.ReadAsStringAsync());
            if (!responseContent.IsValid || !response.IsSuccessStatusCode)
            {
                responseContent.IsValid = false;
                return responseContent;
            }

            var securityToken = new JwtSecurityTokenHandler().ReadJwtToken(responseContent.Data.AccessToken);
            var claims = new List<Claim>();
            claims.Add(new Claim("JWT/Bearer", responseContent.Data.AccessToken));
            claims.Add(new Claim("Refresh/Bearer", responseContent.Data.RefreshToken.Value));
            claims.Add(new Claim(ClaimTypes.Name, responseContent.Data.User.Email));
            claims.AddRange(securityToken.Claims);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTime.Parse(securityToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Expiration).Value)
            };

            await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme)), authProperties);

            return responseContent;
        }

        public async Task LogoutAsync(HttpContext context)
        {
            await context.SignOutAsync();
        }

        public async Task<APIResponseBase> RegisterAsync(RegisterViewModel request)
        {
            var response = await Client.PostAsJsonAsync("/api/auth/login", request);
            return JsonSerializer.Deserialize<APIResponseBase>(await response.Content.ReadAsStringAsync());
        }
    }
}
