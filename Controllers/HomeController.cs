using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TechStore.Models;
using TechStore.Services;

namespace TechStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITechStoreService _storeService;
        private readonly ILogger<HomeController> _logger;

        // Constructor con inyección de dependencias de Servicio y Logger
        public HomeController(ITechStoreService storeService, ILogger<HomeController> logger)
        {
            _storeService = storeService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new HomeView
            {
                Categorias = await _storeService.ObtenerCategoriasAsync(),
                Productos = await _storeService.ObtenerProductosAsync()
            };

            return View(viewModel);
        }

        public IActionResult Privacy() => View();
        public IActionResult Contactenos() => View();
        public IActionResult AboutUs() => View();

        // Manejo centralizado de excepciones (Error 500 / General) + Registro en Serilog
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var exceptionHandlerPathFeature = HttpContext.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerPathFeature>();

            if (exceptionHandlerPathFeature?.Error != null)
            {
                _logger.LogError(exceptionHandlerPathFeature.Error, "Excepción no controlada atrapada en TechStore.");
            }

            var errorViewModel = new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(errorViewModel);
        }

        // Manejo centralizado de códigos de estado HTTP (404, 403) + Auditoría en Serilog
        [Route("Home/StatusCode")]
        public IActionResult StatusCode(int code)
        {
            ViewData["StatusCode"] = code;

            switch (code)
            {
                case 404:
                    _logger.LogWarning("Recurso o ruta no encontrada (404) solicitada por: {User}", User.Identity?.Name ?? "Anónimo");
                    return View("NotFound");

                case 403:
                    _logger.LogWarning("Intento de acceso prohibido (403) por el usuario: {User}", User.Identity?.Name ?? "Anónimo");
                    return View("AccessDenied");

                default:
                    _logger.LogWarning("Código de estado HTTP anómalo procesado: {Code} por {User}", code, User.Identity?.Name ?? "Anónimo");
                    return View("Error");
            }
        }
    }
}