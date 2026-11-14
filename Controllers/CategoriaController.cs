using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechStore.Models;
using TechStore.Services;

namespace TechStore.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly ICategoriaService _categoriaService;
        private readonly ILogger<CategoriaController> _logger;

        public CategoriaController(ICategoriaService categoriaService, ILogger<CategoriaController> logger)
        {
            _categoriaService = categoriaService;
            _logger = logger;
        }

        // Vista pública para explorar categorías y filtrar productos
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var categorias = await _categoriaService.ObtenerTodasAsync();
            return View(categorias);
        }

        // Acciones administrativas protegidas por rol
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                await _categoriaService.CrearAsync(categoria);
                _logger.LogInformation("Categoría '{Nombre}' creada exitosamente por el usuario {Usuario}.", categoria.Nombre, User.Identity?.Name);
                return RedirectToAction(nameof(Index));
            }

            _logger.LogWarning("Intento fallido de creación de categoría con datos inválidos por {Usuario}.", User.Identity?.Name);
            return View(categoria);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var categoria = await _categoriaService.ObtenerPorIdAsync(id);
            if (categoria == null)
            {
                _logger.LogWarning("Intento de editar categoría inexistente con ID {Id} por {Usuario}.", id, User.Identity?.Name);
                return NotFound();
            }
            return View(categoria);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Categoria categoria)
        {
            if (id != categoria.Id)
            {
                _logger.LogWarning("Conflicto de IDs en edición: ID URL {Id} no coincide con ID Modelo {ModeloId} por {Usuario}.", id, categoria.Id, User.Identity?.Name);
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _categoriaService.ActualizarAsync(categoria);
                _logger.LogInformation("Categoría ID {Id} ('{Nombre}') actualizada por {Usuario}.", id, categoria.Nombre, User.Identity?.Name);
                return RedirectToAction(nameof(Index));
            }

            _logger.LogWarning("Intento fallido de actualización de categoría ID {Id} por errores de modelo por {Usuario}.", id, User.Identity?.Name);
            return View(categoria);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var categoria = await _categoriaService.ObtenerPorIdAsync(id);
            if (categoria == null)
            {
                _logger.LogWarning("Intento de acceder a vista Delete de categoría inexistente ID {Id} por {Usuario}.", id, User.Identity?.Name);
                return NotFound();
            }
            return View(categoria);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var (exito, mensaje) = await _categoriaService.EliminarAsync(id);

            if (exito)
                _logger.LogInformation("Categoría ID {Id} eliminada por el administrador {Usuario}.", id, User.Identity?.Name);
            else
                _logger.LogWarning("Fallo al intentar eliminar categoría ID {Id}: {Mensaje} por {Usuario}", id, mensaje, User.Identity?.Name);

            TempData[exito ? "Exito" : "Error"] = mensaje;
            return RedirectToAction(nameof(Index));
        }
    }
}