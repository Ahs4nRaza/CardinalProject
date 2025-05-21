using CardinalProject.Helpers;
using System.Security.Claims;
using CardinalProject.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace CardinalProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("", "Please enter both email and password.");
                return View();
            }

            var user = await _authService.ValidateUserCredentialsAsync(email, password);

            if (user == null)
            {
                ModelState.AddModelError("", "Invalid email or password.");
                return View();
            }

            if (!user.IsActive)
            {
                ModelState.AddModelError("", "Your account is inactive. Please contact the administrator.");
                return View();
            }

            var claims = ClaimsHelper.GetUserClaims(user);
            const string scheme = "UserAuth";
            var identity = new ClaimsIdentity(claims, scheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(scheme, principal);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("UserAuth");
            return RedirectToAction("Login");
        }
    }
}
