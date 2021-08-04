using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NSE.Auth.API.Configuration.Jwt;
using NSE.Auth.API.Requests;
using NSE.Core.Controller.Base;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NSE.Auth.API.Controllers
{
    [Route("api/auth")]
    public class AuthController : ApiController
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _sigInManager;
        private readonly IJwtService _jwtService;

        public AuthController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> sigInManager,
            IJwtService jwtService
        )
        {
            _userManager = userManager;
            _sigInManager = sigInManager;
            _jwtService = jwtService;
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
            if (!result.Succeeded)
            {
                AddError("User", "Invalid credentials");
                return ApiResponse();
            }

            var user = await _userManager.FindByEmailAsync(request.Email);
            var token = await _jwtService.GenerateJsonWebTokenAsync(user);

            return ApiResponse("Success", new
            {
                token = new
                {
                    bearer = token.Bearer,
                    refreshToken = token.RefreshToken,
                    expiress = token.Expiress,
                    claims = token.Claims.Select(x => new { x.Type, x.Value })
                }
            });
        }

        [Authorize]
        [HttpGet("me")]
        public IActionResult GetInfo()
        {
            return Ok(UserId);
        }
    }
}