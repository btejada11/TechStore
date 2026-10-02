using TechStore.Models;

namespace TechStore.Services
{
    public interface ITechStoreService
    {
        Task<IEnumerable<Producto>> ObtenerProductosAsync();
        Task<Producto?> ObtenerProductoPorIdAsync(int id);
        Task GuardarProductoAsync(Producto producto);
        Task EliminarProductoAsync(int id);
        Task<IEnumerable<Categoria>> ObtenerCategoriasAsync();
    }
}
