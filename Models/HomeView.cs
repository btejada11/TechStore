namespace TechStore.Models
{
    public class HomeView
    {
        public List<Categoria> Categorias { get; set; } = new();
        public List<Producto> Productos { get; set; } = new();
    }
}