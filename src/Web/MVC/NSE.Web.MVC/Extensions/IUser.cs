using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NSE.Web.MVC.Extensions
{
    public interface IUser
    {
        Guid Id { get; }
        string Name { get; }
        string Email { get; }
        string Token { get; }
        string RefreshToken { get; }
        bool IsAuthenticated { get; }
        HttpContext Context { get; }
    }

    public class IdentityUser : IUser
    {
        private readonly IHttpContextAccessor HttpContextAccessor;

        public IdentityUser(IHttpContextAccessor context)
        {
            HttpContextAccessor = context;
        }

        public Guid Id => Guid.TryParse(HttpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value, out Guid id) ? id : Guid.Empty;

        public string Name => HttpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;

        public string Email => HttpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

        public string Token => HttpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "JWT/Bearer")?.Value;

        public string RefreshToken => HttpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Refresh/Bearer")?.Value;

        public bool IsAuthenticated => HttpContextAccessor.HttpContext.User.Identity.IsAuthenticated;

        public HttpContext Context => HttpContextAccessor.HttpContext;
    }
}
