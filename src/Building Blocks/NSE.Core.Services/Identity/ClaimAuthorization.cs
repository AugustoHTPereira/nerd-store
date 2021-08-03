using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NSE.Core.Services.Identity
{
    public class ClaimAuthorization
    {
        public static bool Validate(HttpContext context, string claimType, string claimValue)
            => context.User.Identity.IsAuthenticated && context.User.HasClaim(claimType, claimValue);
    }

    public class ClaimAuthorizeAttribute : TypeFilterAttribute
    {
        public ClaimAuthorizeAttribute(string claimType, string claimValue)
            : base(typeof(ClaimAuthorizationFilter))
        { }
    }

    public class ClaimAuthorizationFilter : IAuthorizationFilter
    {
        private readonly Claim Claim;

        public ClaimAuthorizationFilter(Claim claim)
        {
            Claim = claim;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new StatusCodeResult(401);
                return;
            }

            if (!ClaimAuthorization.Validate(context.HttpContext, Claim.Type, Claim.Value))
            {
                context.Result = new StatusCodeResult(403);
            }
        }
    }
}
