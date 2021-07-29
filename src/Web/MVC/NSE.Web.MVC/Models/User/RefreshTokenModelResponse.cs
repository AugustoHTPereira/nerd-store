using System;
using System.Text.Json.Serialization;

namespace NSE.Web.MVC.Models.User
{
    public class RefreshTokenModelResponse
    {
        [JsonPropertyName("token")]
        public string Value { get; set; }

        [JsonPropertyName("expiress")]
        public DateTime Expires { get; set; }
    }
}
