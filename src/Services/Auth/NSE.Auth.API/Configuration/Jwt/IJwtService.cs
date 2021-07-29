using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NSE.Auth.API.Configuration.Jwt
{
    public interface IJwtService
    {
        string GenerateJsonWebToken(IdentityUser user);
    }
}
