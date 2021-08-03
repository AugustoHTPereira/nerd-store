using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NSE.Web.MVC.Models.User
{
    public class LoginModelResponse
    {
        [JsonPropertyName("token")]
        public TokenModelResponse Token { get; set; }
    }

    public class TokenModelResponse
    {
        [JsonPropertyName("bearer")]
        public string Bearer { get; set; }

        [JsonPropertyName("refreshToken")]
        public string RefreshToken { get; set; }

        [JsonPropertyName("expiress")]
        public DateTime Expiress { get; set; }

        [JsonPropertyName("claims")]
        public IEnumerable<ClaimModelResponse> Claims { get; set; }

        public string GetClaimValue(string type)
        {
            return Claims.FirstOrDefault(x => x.Type == type)?.Value ?? string.Empty;
        }
    }

    public class ClaimModelResponse
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }
    }
}
