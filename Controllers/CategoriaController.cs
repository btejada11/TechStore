// Controllers/CategoriaController.cs
using Microsoft.AspNetCore.Mvc;
using TechStore.Services;

namespace TechStore.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly ITechStoreService _storeService;

        // El constructor inyecta correctamente el servicio compartido
        public CategoriaController(ITechStoreService storeService)
        {
            _storeService = storeService;
        }

        // CORRECCIÓN: El método debe ser asíncrono y solicitar los datos al servicio
        public async Task<IActionResult> Index()
        {
            // Solicitamos la lista real a Entity Framework Core
            var categorias = await _storeService.ObtenerCategoriasAsync();

            // Le pasamos la lista de la BD a la vista obligatoriamente
            return View(categorias);
        }
    }
}
