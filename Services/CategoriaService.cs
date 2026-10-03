using Microsoft.EntityFrameworkCore;
using TechStore.Data;
using TechStore.Models;

namespace TechStore.Services
{
    // Implementación real del servicio de categorias
    public class CategoriaService : ICategoriaService
    {
        private readonly ApplicationDbContext _context;

        // Inyectamos el DbContext en el constructor
        public CategoriaService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Consulta asíncrona para traer todas las categorias
        public async Task<IEnumerable<Categoria>> ObtenerTodasAsync()
        {
            return await _context.Categorias.ToListAsync();
        }

        // Busca una categoria en la base de datos por su ID único
        public async Task<Categoria?> ObtenerPorIdAsync(int id)
        {
            return await _context.Categorias.FirstOrDefaultAsync(c => c.Id == id);
        }

        // Guarda un nuevo registro de categoria
        public async Task CrearAsync(Categoria categoria)
        {
            _context.Categorias.Add(categoria);
            await _context.SaveChangesAsync();
        }

        // Aplica los cambios editados de una categoria
        public async Task ActualizarAsync(Categoria categoria)
        {
            _context.Categorias.Update(categoria);
            await _context.SaveChangesAsync();
        }

        // Elimina una categoria si no tiene productos vinculados
        public async Task<(bool Exito, string Mensaje)> EliminarAsync(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null)
            {
                return (false, "La categoría seleccionada no existe o ya fue eliminada.");
            }

            // Cuenta cuantos productos estan asignados a esta categoria
            int cantidadProductos = await _context.Productos.CountAsync(p => p.CategoriaId == id);

            if (cantidadProductos > 0)
            {
                return (false, $"No se puede eliminar la categoría \"{categoria.Nombre}\" porque tiene {cantidadProductos} producto(s) asignado(s). Primero debes reasignar o eliminar esos productos.");
            }

            try
            {
                _context.Categorias.Remove(categoria);
                await _context.SaveChangesAsync();
                return (true, $"La categoría \"{categoria.Nombre}\" se eliminó correctamente.");
            }
            catch (DbUpdateException)
            {
                return (false, "Ocurrió un error en la base de datos al intentar eliminar la categoría.");
            }
        }
    }
}