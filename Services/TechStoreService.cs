using Microsoft.EntityFrameworkCore;
using TechStore.Data;
using TechStore.Models;

namespace TechStore.Services
{
    public class TechStoreService : ITechStoreService
    {
        private readonly ApplicationDbContext _context;

        public TechStoreService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Producto>> ObtenerProductosAsync() =>
            await _context.Productos.Include(p => p.Categoria).ToListAsync();

        public async Task<Producto?> ObtenerProductoPorIdAsync(int id) =>
            await _context.Productos.Include(p => p.Categoria).FirstOrDefaultAsync(p => p.Id == id);

        public async Task GuardarProductoAsync(Producto producto)
        {
            if (producto.Id == 0)
                _context.Productos.Add(producto);
            else
                _context.Productos.Update(producto);

            await _context.SaveChangesAsync();
        }

        public async Task EliminarProductoAsync(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto != null)
            {
                _context.Productos.Remove(producto);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Categoria>> ObtenerCategoriasAsync() =>
            await _context.Categorias.OrderBy(c => c.Nombre).ToListAsync();
    }
}
