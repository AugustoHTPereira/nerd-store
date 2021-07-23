using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NSE.Auth.API.Controllers.Base;
using NSE.Auth.API.Requests;
using System.Threading.Tasks;

namespace NSE.Auth.API.Controllers
{
    [Route("api/auth")]
    public class AuthController : ApiController
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _sigInManager;

        public AuthController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> sigInManager
        )
        {
            _userManager = userManager;
            _sigInManager = sigInManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] UserRegisterRequest request)
        {
            if (!ModelState.IsValid) return UnprocessableEntity(ModelState.Values);

            var user = new IdentityUser
            {
                UserName = request.Email,
                Email = request.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
                return ApiResponse(result.Errors);

            return ApiResponse();
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] UserLoginRequest request)
        {
            if (!ModelState.IsValid) return UnprocessableEntity(ModelState.Values);

            var result = await _sigInManager.PasswordSignInAsync(request.Email, request.Password, false, true);
            if (result.Succeeded)
                AddError("Invalid credentials");

            return ApiResponse();
        }
    }
}