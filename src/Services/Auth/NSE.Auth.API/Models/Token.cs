using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NSE.Auth.API.Models
{
    public class Token
    {
        public Token()
        {
            Claims = new List<Claim>();
        }

        public string Bearer { get; set; }
        public string RefreshToken { get; set; }
        public List<Claim> Claims { get; set; }
        public DateTime Expiress { get; set; }
    }
}
