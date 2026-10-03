// Models/HomeView.cs
namespace TechStore.Models
{
    public class HomeView
    {
        public IEnumerable<Categoria> Categorias { get; set; } = new List<Categoria>();
        public IEnumerable<Producto> Productos { get; set; } = new List<Producto>();
    }
}
