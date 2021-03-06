using System.ComponentModel.DataAnnotations;

namespace NSE.Auth.API.Requests
{
    public class UserRegisterRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string PasswordConfirmation { get; set; }
    }
}