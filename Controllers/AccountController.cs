using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TechStore.Models;

namespace TechStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<AccountController> _logger;

        public AccountController(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            ILogger<AccountController> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
        }

        // GET: Login
        [HttpGet, AllowAnonymous]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        // POST: Login
        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Intento de login con modelo inválido para el correo: {Email}", model.Email);
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(
                model.Email, model.Password, model.RememberMe, lockoutOnFailure: true);

            if (result.Succeeded)
            {
                _logger.LogInformation("Inicio de sesión exitoso para el usuario: {Email}", model.Email);
                return !string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl)
                    ? Redirect(returnUrl)
                    : RedirectToAction("Index", "Home");
            }

            if (result.IsLockedOut)
            {
                _logger.LogWarning("Seguridad: Cuenta bloqueada temporalmente por múltiples intentos fallidos: {Email}", model.Email);
                ModelState.AddModelError("", "Cuenta bloqueada temporalmente debido a intentos fallidos. Intente más tarde.");
            }
            else
            {
                _logger.LogWarning("Intento de login fallido (credenciales incorrectas): {Email}", model.Email);
                ModelState.AddModelError("", "Correo o contraseña incorrectos.");
            }

            return View(model);
        }

        // GET: Register
        [HttpGet, AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        // POST: Register
        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Intento de registro fallido por errores en el modelo.");
                return View(model);
            }

            var user = new IdentityUser
            {
                UserName = model.Email,
                Email = model.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // Por defecto, todo usuario registrado se asigna al rol "Cliente"
                await _userManager.AddToRoleAsync(user, "Cliente");
                await _signInManager.SignInAsync(user, isPersistent: false);

                _logger.LogInformation("Nuevo usuario registrado y autenticado exitosamente: {Email} con rol 'Cliente'.", model.Email);
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors)
            {
                _logger.LogWarning("Error al registrar usuario {Email}: {ErrorDescription}", model.Email, error.Description);
                ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }

        // POST: Logout
        [HttpPost, Authorize, ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            var usuarioActual = User.Identity?.Name;
            _logger.LogInformation("Cierre de sesión para el usuario: {User}", usuarioActual);

            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        // GET: AccessDenied (Manejo de rutas prohibidas - Error 403)
        [HttpGet, AllowAnonymous]
        public IActionResult AccessDenied()
        {
            _logger.LogWarning("Intento de acceso denegado (403) por el usuario: {User}", User.Identity?.Name ?? "Anónimo");
            return View();
        }
    }
}