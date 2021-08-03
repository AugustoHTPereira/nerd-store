using Microsoft.AspNetCore.Identity;
using NSE.Auth.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NSE.Auth.API.Configuration.Jwt
{
    public interface IJwtService
    {
        Task<Token> GenerateJsonWebTokenAsync(IdentityUser user);
    }
}
