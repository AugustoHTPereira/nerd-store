using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NSE.Web.MVC.Models.User
{
    public class LoginModelResponse
    {
        [JsonPropertyName("token")]
        public string AccessToken { get; set; }

        [JsonPropertyName("user")]
        public UserLoginModelResponse User { get; set; }

        [JsonPropertyName("refreshToken")]
        public RefreshTokenModelResponse RefreshToken { get; set; }
    }
}
