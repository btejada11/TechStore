using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TechStore.Models;
using TechStore.Services;

namespace TechStore.Controllers
{
    public class ProductoController : Controller
    {
        private readonly ITechStoreService _storeService;
        private readonly ILogger<ProductoController> _logger;

        public ProductoController(ITechStoreService storeService, ILogger<ProductoController> logger)
        {
            _storeService = storeService;
            _logger = logger;
        }

        // Listado de Productos: catálogo público
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var productos = await _storeService.ObtenerProductosAsync();
            return View(productos);
        }

        // GET: Crear (solo Admin)
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categorias = new SelectList(await _storeService.ObtenerCategoriasAsync(), "Id", "Nombre");
            return View();
        }

        // POST: Crear (solo Admin)
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(Producto producto)
        {
            if (ModelState.IsValid)
            {
                await _storeService.GuardarProductoAsync(producto);
                _logger.LogInformation("Producto '{Nombre}' creado exitosamente por el usuario {Usuario}.", producto.Nombre, User.Identity?.Name);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categorias = new SelectList(await _storeService.ObtenerCategoriasAsync(), "Id", "Nombre", producto.CategoriaId);
            return View(producto);
        }

        // GET: Editar (solo Admin)
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var producto = await _storeService.ObtenerProductoPorIdAsync(id);
            if (producto == null)
            {
                _logger.LogWarning("Intento de editar un producto inexistente con ID {Id} por {Usuario}.", id, User.Identity?.Name);
                return NotFound();
            }

            ViewBag.Categorias = new SelectList(await _storeService.ObtenerCategoriasAsync(), "Id", "Nombre", producto.CategoriaId);
            return View(producto);
        }

        // POST: Editar (solo Admin)
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, Producto producto)
        {
            if (id != producto.Id) return NotFound();

            if (ModelState.IsValid)
            {
                await _storeService.GuardarProductoAsync(producto);
                _logger.LogInformation("Producto ID {Id} ('{Nombre}') actualizado por {Usuario}.", id, producto.Nombre, User.Identity?.Name);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categorias = new SelectList(await _storeService.ObtenerCategoriasAsync(), "Id", "Nombre", producto.CategoriaId);
            return View(producto);
        }

        // GET: Eliminar (solo Admin)
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var producto = await _storeService.ObtenerProductoPorIdAsync(id);
            if (producto == null) return NotFound();

            return View(producto);
        }

        // POST: Eliminar (solo Admin)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producto = await _storeService.ObtenerProductoPorIdAsync(id);
            if (producto == null) return NotFound();

            await _storeService.EliminarProductoAsync(id);
            _logger.LogWarning("Producto ID {Id} ('{Nombre}') eliminado por el administrador {Usuario}.", id, producto.Nombre, User.Identity?.Name);

            TempData["Eliminado"] = $"El producto \"{producto.Nombre}\" fue eliminado correctamente.";
            return RedirectToAction(nameof(Index));
        }

        // POST: Comprar (Admin y Cliente)
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Cliente")]
        public async Task<IActionResult> Comprar(int id)
        {
            var exito = await _storeService.ComprarProductoAsync(id);
            if (exito)
            {
                _logger.LogInformation("Compra realizada con éxito para el producto ID {Id} por el usuario {Usuario}.", id, User.Identity?.Name);
            }
            else
            {
                _logger.LogWarning("Intento fallido de compra para el producto ID {Id} (agotado o inexistente) por {Usuario}.", id, User.Identity?.Name);
            }

            TempData[exito ? "Exito" : "Error"] = exito
                ? "¡Compra realizada con éxito!"
                : "No se pudo completar la compra: producto agotado o inexistente.";
            return RedirectToAction(nameof(Index));
        }
    }
}