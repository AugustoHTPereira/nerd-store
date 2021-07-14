using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NSE.Auth.API.Requests;
using System.Threading.Tasks;

namespace NSE.Auth.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
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
        public async Task<IActionResult> RegisterAsync(UserRegisterRequest request)
        {
            if (!ModelState.IsValid) return UnprocessableEntity(ModelState.Values);

            var user = new IdentityUser
            {
                UserName = request.Email,
                Email = request.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
                return Ok();

            return BadRequest(result.Errors);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(UserLoginRequest request)
        {
            if (!ModelState.IsValid) return UnprocessableEntity(ModelState.Values);

            var result = await _sigInManager.PasswordSignInAsync(request.Email, request.Password, false, true);
            if (result.Succeeded)
                return Ok();

            return BadRequest("Invalid credentials");
        }
    }
}