using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TechStore.Models;
using TechStore.Services;

namespace TechStore.Controllers
{
    public class ProductoController : Controller
    {
        private readonly ITechStoreService _storeService;

        public ProductoController(ITechStoreService storeService)
        {
            _storeService = storeService;
        }

        // Listado de Productos
        public async Task<IActionResult> Index()
        {
            var productos = await _storeService.ObtenerProductosAsync();
            return View(productos);
        }

        // GET: Crear
        public async Task<IActionResult> Create()
        {
            ViewBag.Categorias = new SelectList(await _storeService.ObtenerCategoriasAsync(), "Id", "Nombre");
            return View();
        }

        // POST: Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Producto producto)
        {
            if (ModelState.IsValid)
            {
                await _storeService.GuardarProductoAsync(producto);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categorias = new SelectList(await _storeService.ObtenerCategoriasAsync(), "Id", "Nombre", producto.CategoriaId);
            return View(producto);
        }

        // GET: Editar
        public async Task<IActionResult> Edit(int id)
        {
            var producto = await _storeService.ObtenerProductoPorIdAsync(id);
            if (producto == null) return NotFound();

            ViewBag.Categorias = new SelectList(await _storeService.ObtenerCategoriasAsync(), "Id", "Nombre", producto.CategoriaId);
            return View(producto);
        }

        // POST: Editar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Producto producto)
        {
            if (id != producto.Id) return NotFound();

            if (ModelState.IsValid)
            {
                await _storeService.GuardarProductoAsync(producto);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categorias = new SelectList(await _storeService.ObtenerCategoriasAsync(), "Id", "Nombre", producto.CategoriaId);
            return View(producto);
        }

        // GET: Eliminar (página de respaldo, la confirmación principal es el SweetAlert del listado)
        public async Task<IActionResult> Delete(int id)
        {
            var producto = await _storeService.ObtenerProductoPorIdAsync(id);
            if (producto == null) return NotFound();

            return View(producto);
        }

        // POST: Eliminar
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producto = await _storeService.ObtenerProductoPorIdAsync(id);
            if (producto == null) return NotFound();

            await _storeService.EliminarProductoAsync(id);
            TempData["Eliminado"] = $"El producto \"{producto.Nombre}\" fue eliminado correctamente.";
            return RedirectToAction(nameof(Index));
        }

        // POST: Comprar (descuenta una unidad del stock)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Comprar(int id)
        {
            var exito = await _storeService.ComprarProductoAsync(id);
            TempData[exito ? "Exito" : "Error"] = exito
                ? "¡Compra realizada con éxito!"
                : "No se pudo completar la compra: producto agotado o inexistente.";
            return RedirectToAction(nameof(Index));
        }
    }
}