using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TechStore.Models;
using TechStore.Services; // <- Asegúrate de que este using esté incluido

namespace TechStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITechStoreService _storeService;

        // Constructor que inyecta el servicio de la base de datos
        public HomeController(ITechStoreService storeService)
        {
            _storeService = storeService;
        }

        // El método Index ahora es asíncrono (Task<IActionResult>)
        public async Task<IActionResult> Index()
        {
            var viewModel = new HomeView
            {
                // Cargamos los datos reales desde Entity Framework Core de forma asíncrona
                Categorias = await _storeService.ObtenerCategoriasAsync(),
                Productos = await _storeService.ObtenerProductosAsync()
            };

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Contactenos()
        {
            return View();
        }

        public IActionResult AboutUs()
        {
            return View();
        }
    }
}
