using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NSE.Web.MVC.Models.Base;
using NSE.Web.MVC.Models.User;
using NSE.Web.MVC.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NSE.Web.MVC.Controllers
{
    public class IdentityController : Controller
    {
        private readonly IAuthService AuthService;

        public IdentityController(IAuthService authService)
        {
            AuthService = authService;
        }

        [HttpGet]
        [Route("register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterViewModel request)
        {
            if (!ModelState.IsValid) return View(request);

            var response = await AuthService.RegisterAsync(request);
            if (!response.IsValid)
            {
                response.Errors.ToList().ForEach(x => ModelState.AddModelError(x.Key, x.Value));
                return View(request);
            }

            return RedirectToAction("login");
        }

        [HttpGet]
        [Route("login")]
        public IActionResult Login(string returnTo = null)
        {
            ViewData["returnTo"] = returnTo;
            return View();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginViewModel request, string returnTo = null)
        {
            if (!ModelState.IsValid) return View(request);

            var response = await AuthService.LoginAsync(HttpContext, request);
            if (!response.IsValid)
            {
                response.Errors.ToList().ForEach(x => ModelState.AddModelError(x.Key, x.Value));
                return View(request);
            }

            if (!string.IsNullOrEmpty(returnTo))
                return LocalRedirect(returnTo);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await AuthService.LogoutAsync(HttpContext);
            return RedirectToAction("Login", "Identity");
        }
    }
}
