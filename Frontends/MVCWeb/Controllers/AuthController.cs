using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCWeb.Models;
using MVCWeb.Services;
using MVCWeb.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWeb.Controllers
{
    public class AuthController : Controller
    {
        private readonly IIdentiyService _identityService;
        public AuthController(IIdentiyService identiyService)
        {
            _identityService = identiyService;
        }
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(SigninInput signinInput)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var response = await _identityService.SignIn(signinInput);
            if (!response.IsSuccessful)
            {
                response.Errors.ForEach(x =>
                {
                    ModelState.AddModelError(string.Empty, x);
                });
                return View();
            }
            return RedirectToAction("Privacy", "Home");
        }
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await _identityService.RevokeRefreshToken();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
