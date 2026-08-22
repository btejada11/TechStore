

using Microsoft.AspNetCore.Mvc;
using TechStore.Models;
namespace TechStore.Controllers
{
    /// <summary>
    /// Controlador para manejar las operaciones relacionadas con los productos en la tienda.
    /// </summary>
    public class ProductoController : Controller
    {

        public static readonly List<Models.Producto> ListaProducto = new List<Models.Producto>
        {
            // REDES
                new Producto { Id = 1, Nombre = "Router TP-Link AX1800", Descripcion = "Router WiFi 6 de alta velocidad", Precio = 89.99m, Categoria = Categorias.Redes , Estado = Estado.Nuevo, Imagen = "https://acosa.com.sv/wp-content/uploads/2025/06/Archer20AX20.webp" },
                new Producto { Id = 2, Nombre = "Switch Cisco 8 Puertos", Descripcion = "Switch administrable empresarial", Precio = 149.99m, Categoria = Categorias.Redes, Estado = Estado.Open_Box, Imagen = "https://m.media-amazon.com/images/I/81w-m0JPtwL._AC_SX300_SY300_QL70_FMwebp_.jpg" },
                new Producto { Id = 3, Nombre = "Access Point Ubiquiti", Descripcion = "Punto de acceso inalámbrico", Precio = 129.99m, Categoria = Categorias.Redes, Estado = Estado.Nuevo, Imagen = "https://computodo.com.sv/wp-content/uploads/2025/10/Ubiquiti-Wireless-Access-Point-U7-Lite-Compact-Ceiling-Mounted-computodo-El-Salvador.jpeg" },
                new Producto { Id = 4, Nombre = "Extensor WiFi TP-Link", Descripcion = "Amplificador de señal", Precio = 39.99m, Categoria = Categorias.Redes, Estado = Estado.Nuevo, Imagen = "https://acosa.com.sv/wp-content/uploads/2022/11/RE305.webp" },
                new Producto { Id = 5, Nombre = "Cable Cat6 100m", Descripcion = "Cable de red profesional", Precio = 69.99m, Categoria = Categorias.Redes, Estado = Estado.Open_Box, Imagen = "https://www.mcgillmicrowave.com/wp-content/uploads/2021/06/cat-6-ethernet-cable.webp" },
                new Producto { Id = 6, Nombre = "Tarjeta de Red Gigabit", Descripcion = "Adaptador PCI Express", Precio = 24.99m, Categoria = Categorias.Redes, Estado = Estado.Nuevo, Imagen = "https://acosa.com.sv/wp-content/uploads/2025/05/TG-3468UN.webp " },

                // COMPUTADORAS
                new Producto { Id = 7, Nombre = "Laptop Dell XPS 13", Descripcion = "Laptop ultradelgada", Precio = 999.99m, Categoria = Categorias.Computadoras, Estado = Estado.Nuevo, Imagen = "https://i.dell.com/is/image/DellContent/content/dam/ss2/product-images/dell-client-products/notebooks/xps-notebooks/9345/media-gallery/touch/silver/xps-13-9345-laptop-silver-copilot-pc-mg.png?fmt=png-alpha&pscan=auto&scl=1&hei=476&wid=725&qlt=100,1&resMode=sharp2&size=725,476&chrss=full" },
                new Producto { Id = 8, Nombre = "MacBook Air M2", Descripcion = "Laptop Apple portátil", Precio = 1199.99m, Categoria = Categorias.Computadoras, Estado = Estado.Open_Box, Imagen = "https://www.apple.com/newsroom/images/product/mac/standard/Apple-WWDC22-MacBook-Air-hero-220606_big.jpg.medium.jpg" },
                new Producto { Id = 9, Nombre = "PC Gamer Ryzen 7", Descripcion = "Equipo para gaming", Precio = 1299.99m, Categoria = Categorias.Computadoras, Estado = Estado.Nuevo, Imagen = "https://api.zonadigitalsv.com/storage/products/imagen_generada6a68f4c6b2a76.jpg" },
                new Producto { Id = 10, Nombre = "Mini PC Intel NUC", Descripcion = "Computadora compacta", Precio = 499.99m, Categoria = Categorias.Computadoras, Estado = Estado.Nuevo, Imagen = "https://m.media-amazon.com/images/I/71CB2TTNqcL._AC_SY355_.jpg" },
                new Producto { Id = 11, Nombre = "Teclado Logitech G512", Descripcion = "Teclado mecánico RGB", Precio = 129.99m, Categoria = Categorias.Computadoras, Estado = Estado.Open_Box, Imagen = "https://resource.logitechg.com/w_544,h_466,ar_7:6,c_pad,q_auto,f_auto,dpr_1.0/d_transparent.gif/content/dam/gaming/en/non-braid/flying-v-mx-g512/g512-keyboard-gallery-2-2x-nb.png" },
                new Producto { Id = 12, Nombre = "Mouse Razer DeathAdder", Descripcion = "Mouse gamer ergonómico", Precio = 79.99m, Categoria = Categorias.Computadoras, Estado = Estado.Nuevo, Imagen = "https://tiendaintelmax.net/images/productos/mouse-gaming-razer-deathadder-essential-black-rz0103850100r3u1mou0410.png" },

                // TELEFONÍA
                new Producto { Id = 13, Nombre = "iPhone 15", Descripcion = "Smartphone Apple", Precio = 999.99m, Categoria = Categorias.Telefonia, Estado = Estado.Nuevo, Imagen = "https://www.radioshackla.com/media/catalog/product/4/6/464407200010_2_.jpg?optimize=medium&bg-color=255,255,255&fit=bounds&height=&width=&canvas=:" },
                new Producto { Id = 14, Nombre = "Samsung Galaxy S24", Descripcion = "Teléfono Android premium", Precio = 899.99m, Categoria = Categorias.Telefonia, Estado = Estado.Open_Box, Imagen = "https://buketomnisportpweb.s3.us-east-2.amazonaws.com/products-images/4gaOtmobLv4CTncFwNqD1DTfcHXX5k2bBzzz9QVQ.png" },
                new Producto { Id = 15, Nombre = "Google Pixel 8", Descripcion = "Smartphone con IA", Precio = 799.99m, Categoria = Categorias.Telefonia, Estado = Estado.Nuevo, Imagen = "https://i5.walmartimages.com/seo/Google-Pixel-8-Pro-5G-128GB-Smartphone-Unlocked-Bay_63385478-cc05-46f9-89ca-5fb1cd788092.1f4fb647712625f91c7fa9e42e2c0bef.jpeg" },
                new Producto { Id = 16, Nombre = "Xiaomi Redmi Note 13", Descripcion = "Excelente rendimiento", Precio = 299.99m, Categoria = Categorias.Telefonia, Estado = Estado.Nuevo, Imagen = "https://buketomnisportpweb.s3.us-east-2.amazonaws.com/products-thumbs/gUqCyfKTzlY5gmkAZtWnq58nJCb3KgmHLemC4Zcs.png" },
                new Producto { Id = 17, Nombre = "Motorola Edge", Descripcion = "Pantalla OLED", Precio = 549.99m, Categoria = Categorias.Telefonia, Estado = Estado.Open_Box, Imagen = "https://m.media-amazon.com/images/I/712nxo9u15L._AC_SY300_SX300_QL70_FMwebp_.jpg" },
                new Producto { Id = 18, Nombre = "Samsung Galaxy A55", Descripcion = "Gama media avanzada", Precio = 399.99m, Categoria = Categorias.Telefonia, Estado = Estado.Nuevo, Imagen = "https://i.ebayimg.com/images/g/TT8AAOSw00xmr9Aa/s-l960.webp" },

                // AUDÍFONOS
                new Producto { Id = 19, Nombre = "Sony WH-1000XM5", Descripcion = "Cancelación de ruido premium", Precio = 399.99m, Categoria = Categorias.Audifonos, Estado = Estado.Nuevo, Imagen = "https://buketomnisportpweb.s3.us-east-2.amazonaws.com/products-images/8aYuEQ2SxiQELceSJ1rPNswB6FrnpUXUOTbcMsz9.jpeg" },
                new Producto { Id = 20, Nombre = "AirPods Pro", Descripcion = "Audífonos inalámbricos Apple", Precio = 249.99m, Categoria = Categorias.Audifonos, Estado = Estado.Open_Box, Imagen = "https://siman.vtexassets.com/arquivos/ids/7334275-1200-auto?v=638950204181600000&width=1200&height=auto&aspect=true" },
                new Producto { Id = 21, Nombre = "JBL Tune 760NC", Descripcion = "Bluetooth con ANC", Precio = 119.99m, Categoria = Categorias.Audifonos, Estado = Estado.Nuevo, Imagen = "https://i5.walmartimages.com/seo/JBL-Tune-760NC-Wireless-Over-Ear-NC-Headphones-Black_c22e4de6-53d0-4c0c-b067-9a853564d980.69cdda1e793c4305cd96b1d82824b47d.jpeg" },
                new Producto { Id = 22, Nombre = "HyperX Cloud II", Descripcion = "Audífonos gamer", Precio = 89.99m, Categoria = Categorias.Audifonos, Estado = Estado.Nuevo, Imagen = "https://m.media-amazon.com/images/I/61PzYfuwOML._AC_SL1500_.jpg" }


        };
        public IActionResult Index()
            
        {
            var productos = new List<Producto>

      
            {
                // REDES
                new Producto { Id = 1, Nombre = "Router TP-Link AX1800", Descripcion = "Router WiFi 6 de alta velocidad", Precio = 89.99m, Categoria = Categorias.Redes , Estado = Estado.Nuevo, Imagen = "https://acosa.com.sv/wp-content/uploads/2025/06/Archer20AX20.webp" },
                new Producto { Id = 2, Nombre = "Switch Cisco 8 Puertos", Descripcion = "Switch administrable empresarial", Precio = 149.99m, Categoria = Categorias.Redes, Estado = Estado.Open_Box, Imagen = "https://m.media-amazon.com/images/I/81w-m0JPtwL._AC_SX300_SY300_QL70_FMwebp_.jpg" },
                new Producto { Id = 3, Nombre = "Access Point Ubiquiti", Descripcion = "Punto de acceso inalámbrico", Precio = 129.99m, Categoria = Categorias.Redes, Estado = Estado.Nuevo, Imagen = "https://computodo.com.sv/wp-content/uploads/2025/10/Ubiquiti-Wireless-Access-Point-U7-Lite-Compact-Ceiling-Mounted-computodo-El-Salvador.jpeg" },
                new Producto { Id = 4, Nombre = "Extensor WiFi TP-Link", Descripcion = "Amplificador de señal", Precio = 39.99m, Categoria = Categorias.Redes, Estado = Estado.Nuevo, Imagen = "https://acosa.com.sv/wp-content/uploads/2022/11/RE305.webp" },
                new Producto { Id = 5, Nombre = "Cable Cat6 100m", Descripcion = "Cable de red profesional", Precio = 69.99m, Categoria = Categorias.Redes, Estado = Estado.Open_Box, Imagen = "https://www.mcgillmicrowave.com/wp-content/uploads/2021/06/cat-6-ethernet-cable.webp" },
                new Producto { Id = 6, Nombre = "Tarjeta de Red Gigabit", Descripcion = "Adaptador PCI Express", Precio = 24.99m, Categoria = Categorias.Redes, Estado = Estado.Nuevo, Imagen = "https://acosa.com.sv/wp-content/uploads/2025/05/TG-3468UN.webp " },

                // COMPUTADORAS
                new Producto { Id = 7, Nombre = "Laptop Dell XPS 13", Descripcion = "Laptop ultradelgada", Precio = 999.99m, Categoria = Categorias.Computadoras, Estado = Estado.Nuevo, Imagen = "https://i.dell.com/is/image/DellContent/content/dam/ss2/product-images/dell-client-products/notebooks/xps-notebooks/9345/media-gallery/touch/silver/xps-13-9345-laptop-silver-copilot-pc-mg.png?fmt=png-alpha&pscan=auto&scl=1&hei=476&wid=725&qlt=100,1&resMode=sharp2&size=725,476&chrss=full" },
                new Producto { Id = 8, Nombre = "MacBook Air M2", Descripcion = "Laptop Apple portátil", Precio = 1199.99m, Categoria = Categorias.Computadoras, Estado = Estado.Open_Box, Imagen = "https://www.apple.com/newsroom/images/product/mac/standard/Apple-WWDC22-MacBook-Air-hero-220606_big.jpg.medium.jpg" },
                new Producto { Id = 9, Nombre = "PC Gamer Ryzen 7", Descripcion = "Equipo para gaming", Precio = 1299.99m, Categoria = Categorias.Computadoras, Estado = Estado.Nuevo, Imagen = "https://api.zonadigitalsv.com/storage/products/imagen_generada6a68f4c6b2a76.jpg" },
                new Producto { Id = 10, Nombre = "Mini PC Intel NUC", Descripcion = "Computadora compacta", Precio = 499.99m, Categoria = Categorias.Computadoras, Estado = Estado.Nuevo, Imagen = "https://m.media-amazon.com/images/I/71CB2TTNqcL._AC_SY355_.jpg" },
                new Producto { Id = 11, Nombre = "Teclado Logitech G512", Descripcion = "Teclado mecánico RGB", Precio = 129.99m, Categoria = Categorias.Computadoras, Estado = Estado.Open_Box, Imagen = "https://resource.logitechg.com/w_544,h_466,ar_7:6,c_pad,q_auto,f_auto,dpr_1.0/d_transparent.gif/content/dam/gaming/en/non-braid/flying-v-mx-g512/g512-keyboard-gallery-2-2x-nb.png" },
                new Producto { Id = 12, Nombre = "Mouse Razer DeathAdder", Descripcion = "Mouse gamer ergonómico", Precio = 79.99m, Categoria = Categorias.Computadoras, Estado = Estado.Nuevo, Imagen = "https://tiendaintelmax.net/images/productos/mouse-gaming-razer-deathadder-essential-black-rz0103850100r3u1mou0410.png" },

                // TELEFONÍA
                new Producto { Id = 13, Nombre = "iPhone 15", Descripcion = "Smartphone Apple", Precio = 999.99m, Categoria = Categorias.Telefonia, Estado = Estado.Nuevo, Imagen = "https://www.radioshackla.com/media/catalog/product/4/6/464407200010_2_.jpg?optimize=medium&bg-color=255,255,255&fit=bounds&height=&width=&canvas=:" },
                new Producto { Id = 14, Nombre = "Samsung Galaxy S24", Descripcion = "Teléfono Android premium", Precio = 899.99m, Categoria = Categorias.Telefonia, Estado = Estado.Open_Box, Imagen = "https://buketomnisportpweb.s3.us-east-2.amazonaws.com/products-images/4gaOtmobLv4CTncFwNqD1DTfcHXX5k2bBzzz9QVQ.png" },
                new Producto { Id = 15, Nombre = "Google Pixel 8", Descripcion = "Smartphone con IA", Precio = 799.99m, Categoria = Categorias.Telefonia, Estado = Estado.Nuevo, Imagen = "https://i5.walmartimages.com/seo/Google-Pixel-8-Pro-5G-128GB-Smartphone-Unlocked-Bay_63385478-cc05-46f9-89ca-5fb1cd788092.1f4fb647712625f91c7fa9e42e2c0bef.jpeg" }, 
                new Producto { Id = 16, Nombre = "Xiaomi Redmi Note 13", Descripcion = "Excelente rendimiento", Precio = 299.99m, Categoria = Categorias.Telefonia, Estado = Estado.Nuevo, Imagen = "https://buketomnisportpweb.s3.us-east-2.amazonaws.com/products-thumbs/gUqCyfKTzlY5gmkAZtWnq58nJCb3KgmHLemC4Zcs.png" },
                new Producto { Id = 17, Nombre = "Motorola Edge", Descripcion = "Pantalla OLED", Precio = 549.99m, Categoria = Categorias.Telefonia, Estado = Estado.Open_Box, Imagen = "https://m.media-amazon.com/images/I/712nxo9u15L._AC_SY300_SX300_QL70_FMwebp_.jpg" },
                new Producto { Id = 18, Nombre = "Samsung Galaxy A55", Descripcion = "Gama media avanzada", Precio = 399.99m, Categoria = Categorias.Telefonia, Estado = Estado.Nuevo, Imagen = "https://i.ebayimg.com/images/g/TT8AAOSw00xmr9Aa/s-l960.webp" },

                // AUDÍFONOS
                new Producto { Id = 19, Nombre = "Sony WH-1000XM5", Descripcion = "Cancelación de ruido premium", Precio = 399.99m, Categoria = Categorias.Audifonos, Estado = Estado.Nuevo, Imagen = "https://buketomnisportpweb.s3.us-east-2.amazonaws.com/products-images/8aYuEQ2SxiQELceSJ1rPNswB6FrnpUXUOTbcMsz9.jpeg" },
                new Producto { Id = 20, Nombre = "AirPods Pro", Descripcion = "Audífonos inalámbricos Apple", Precio = 249.99m, Categoria = Categorias.Audifonos, Estado = Estado.Open_Box, Imagen = "https://siman.vtexassets.com/arquivos/ids/7334275-1200-auto?v=638950204181600000&width=1200&height=auto&aspect=true" },
                new Producto { Id = 21, Nombre = "JBL Tune 760NC", Descripcion = "Bluetooth con ANC", Precio = 119.99m, Categoria = Categorias.Audifonos, Estado = Estado.Nuevo, Imagen = "https://i5.walmartimages.com/seo/JBL-Tune-760NC-Wireless-Over-Ear-NC-Headphones-Black_c22e4de6-53d0-4c0c-b067-9a853564d980.69cdda1e793c4305cd96b1d82824b47d.jpeg" },
                new Producto { Id = 22, Nombre = "HyperX Cloud II", Descripcion = "Audífonos gamer", Precio = 89.99m, Categoria = Categorias.Audifonos, Estado = Estado.Nuevo, Imagen = "https://m.media-amazon.com/images/I/61PzYfuwOML._AC_SL1500_.jpg" }
            };

            return View(productos);
        }
    }
}
