using HieuLongPortfolio.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HieuLongPortfolio.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IConfiguration _configuration;


        public AdminController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        // =========================================
        // ADMIN DASHBOARD
        // =========================================

        public IActionResult Index()
        {
            return View();
        }


        // =========================================
        // LOGIN - GET
        // =========================================

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            // If already logged in, go straight to Admin.
            if (User.Identity?.IsAuthenticated == true)
            {
                return RedirectToAction(nameof(Index));
            }


            var model = new AdminLoginViewModel
            {
                ReturnUrl = returnUrl
            };


            return View(model);
        }


        // =========================================
        // LOGIN - POST
        // =========================================

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AdminLoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }


            // Fixed admin username.
            const string adminUsername = "admin";


            // Password comes from User Secrets / environment.
            var adminPassword =
                _configuration["AdminCredentials:Password"];


            // Make sure the password was actually configured.
            if (string.IsNullOrWhiteSpace(adminPassword))
            {
                ModelState.AddModelError(
                    string.Empty,
                    "Admin login is not configured.");

                return View(model);
            }


            // Check credentials.
            if (model.Username != adminUsername ||
                model.Password != adminPassword)
            {
                ModelState.AddModelError(
                    string.Empty,
                    "Invalid username or password.");

                return View(model);
            }


            // =========================================
            // CREATE USER IDENTITY
            // =========================================

            var claims = new List<Claim>
            {
                new Claim(
                    ClaimTypes.Name,
                    adminUsername),

                new Claim(
                    ClaimTypes.Role,
                    "Admin")
            };


            var identity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme);


            var principal = new ClaimsPrincipal(identity);


            var authenticationProperties =
                new AuthenticationProperties
                {
                    IsPersistent = false,

                    ExpiresUtc =
                        DateTimeOffset.UtcNow.AddHours(8)
                };


            // =========================================
            // SIGN IN
            // =========================================

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                authenticationProperties);


            // =========================================
            // RETURN TO ORIGINAL PAGE
            // =========================================

            if (!string.IsNullOrWhiteSpace(model.ReturnUrl) &&
                Url.IsLocalUrl(model.ReturnUrl))
            {
                return LocalRedirect(model.ReturnUrl);
            }


            return RedirectToAction(nameof(Index));
        }


        // =========================================
        // LOGOUT
        // =========================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);


            return RedirectToAction(nameof(Login));
        }
    }
}