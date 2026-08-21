using Microsoft.AspNetCore.Mvc;

namespace TechStore.Controllers
{

    public class CategoriaController : Controller
    {
        /// <summary>
        /// Lista de categorías de productos disponibles en la tienda.
        /// </summary>

        public static readonly List<Models.Categoria> ListaCategorias = new List<Models.Categoria>
        {
            new Models.Categoria { Id = 1, Nombre = "Redes", Imagen = "https://api.zonadigitalsv.com/storage/products/imagen_generada6a6932f93b1f9.jpg", Descripcion = "Productos relacionados con redes y conectividad." },
            new Models.Categoria { Id = 2, Nombre = "Computadoras", Imagen = "https://dataflexstore.com/cdn/shop/articles/dell-dpbXgTh0Lac-unsplash_1_1080x.jpg?v=1706302745.jpg", Descripcion = "Productos relacionados con computadoras y laptops." },
            new Models.Categoria { Id = 3, Nombre = "Telefonía", Imagen = "https://www.imagar.com/wp-content/uploads/2021/06/analista_programador.jpg", Descripcion = "Productos relacionados con teléfonos y accesorios." },
            new Models.Categoria { Id = 4, Nombre = "Audífonos", Imagen = "https://selectsound.com.mx/cdn/shop/files/audifonos-inalambricos-bluetooth-pantalla-touch-modelo-ss-play-select-sound-bth050-321429_1080x1080.jpg?v=1741968264", Descripcion = "Productos relacionados con audífonos y auriculares." },
            new Models.Categoria { Id = 5, Nombre = "Monitores", Imagen = "https://tiendaintelmax.net/images/productos/monitor-aoc-27-27g50f-1920x1080-144hz-lcd-hdmidisplayportmon0538.jpg", Descripcion = "Productos relacionados con monitores y pantallas." }
        };
        public IActionResult Index()
        {
            return View(ListaCategorias);
        }
    }
}
