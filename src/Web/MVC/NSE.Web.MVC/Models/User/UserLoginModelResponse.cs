using System.Text.Json.Serialization;

namespace NSE.Web.MVC.Models.User
{
    public class UserLoginModelResponse
    {
        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("phoneNumber")]
        public string Phone { get; set; }
    }
}
