using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using NSE.Auth.API.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TokenOptions = NSE.Core.Services.Identity.TokenOptions;

namespace NSE.Auth.API.Configuration.Jwt
{
    public class JwtService : IJwtService
    {
        private readonly TokenOptions Options;
        private readonly UserManager<IdentityUser> UserManager;

        public JwtService(TokenOptions options, UserManager<IdentityUser> userManager)
        {
            Options = options;
            UserManager = userManager;
        }

        public async Task<Token> GenerateJsonWebTokenAsync(IdentityUser user)
        {
            var token = new Token();
            token.Expiress = DateTime.UtcNow.AddHours(Options.Expiress);
            token.RefreshToken = Guid.NewGuid().ToString("N");
            token.Claims.Add(new Claim(ClaimTypes.Email, user.Email));
            token.Claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            token.Claims.Add(new Claim(ClaimTypes.Expiration, token.Expiress.ToString()));

            foreach (var claim in await UserManager.GetClaimsAsync(user))
                token.Claims.Add(claim);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Options.Key);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(token.Claims),
                Expires = token.Expiress,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = Options.Issuer,
                Audience = Options.Audience
            };

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            token.Bearer = tokenHandler.WriteToken(securityToken);

            return token;
        }
    }
}
