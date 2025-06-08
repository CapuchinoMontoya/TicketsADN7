using INTELISIS.APPCORE.EL;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TicketsADN7.Models;
using TicketsADN7.Services;

namespace TicketsADN7.Controllers
{
    public class AccountController : Controller
    {
        private readonly AuthService _authService;
        private readonly TicketsContext _context;

        public AccountController(AuthService authService, TicketsContext context)
        {
            _authService = authService;
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var result = await _authService.LoginAsync(model);

            if(result.Usuario.Resultado != "Error")
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, result.Usuario.NombreUsuario),
                    new Claim(ClaimTypes.Role, result.Usuario.RolID.ToString()),
                    new Claim("Token", result.Token)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties();

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), authProperties);

                return RedirectToAction("Index", "Home");
            }
            ViewBag.Mensaje = result.Usuario.Mensaje;
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}