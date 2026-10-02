using Microsoft.EntityFrameworkCore;
using TechStore.Models;

namespace TechStore.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurar la relación Uno a Muchos
            modelBuilder.Entity<Producto>()
                .HasOne(p => p.Categoria)
                .WithMany(c => c.Productos)
                .HasForeignKey(p => p.CategoriaId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}


/*-- ----------------------------------------------------

USE [TechStoreDb];
GO

-- 1. LIMPIEZA PREVIA
DELETE FROM [Productos];
DELETE FROM [Categorias];

DBCC CHECKIDENT ('[Categorias]', RESEED, 0);
DBCC CHECKIDENT ('[Productos]', RESEED, 0);
GO

-- 2. INYECCIÓN EN LA TABLA CATEGORIAS
INSERT INTO [Categorias] ([Nombre], [Imagen], [Descripcion]) VALUES
('Redes', 'https://www.cualesmiip.com/media/cualesmiip/image/noticias/original/25702_Foto.1756319273.jpeg', 'Productos relacionados con redes y conectividad.'),
('Computadoras', 'https://www.esgamingpc.com/lifisher-m5725/1760603909597-01/jpg100-t3-scale100.jpg', 'Productos relacionados con computadoras y laptops.'),
('Telefonía', 'https://www.imagar.com/wp-content/uploads/2021/06/analista_programador.jpg', 'Productos relacionados con teléfonos y accesorios.'),
('Audífonos', 'https://selectsound.com.mx/cdn/shop/files/audifonos-inalambricos-bluetooth-manos-libres-sense-bth028-511478.jpg?v=1718036754&width=1000', 'Productos relacionados con audífonos y auriculares.'),
('Monitores', 'https://api.zonadigitalsv.com/storage/products/imagen_generada6a567a5b3cb17.jpg', 'Productos relacionados con monitores y pantallas.');
GO

-- 3. INYECCIÓN EN LA TABLA PRODUCTOS
INSERT INTO [Productos] ([Nombre], [Descripcion], [Precio], [CategoriaId], [Estado], [Imagen]) VALUES
-- CATEGORÍA: REDES (CategoriaId = 1)
('Router TP-Link AX1800', 'Router WiFi 6 de alta velocidad', 89.99, 1, 1, 'https://acosa.com.sv/wp-content/uploads/2025/06/Archer20AX20.webp'),
('Switch Cisco 8 Puertos', 'Switch administrable empresarial', 149.99, 1, 2, 'https://www.officeeasy.fr/media/catalog/product/cache/16822256cee3c919bf29bfc0144d0975/c/i/cisco_sf302g-08.jpg'),
('Access Point Ubiquiti', 'Punto de acceso inalámbrico', 129.99, 1, 1, 'https://computodo.com.sv/wp-content/uploads/2025/01/U6.png'),
('Extensor WiFi TP-Link', 'Amplificador de señal', 39.99, 1, 1, 'https://acosa.com.sv/wp-content/uploads/2025/06/RE650.webp'),
('Cable Cat6 100m', 'Cable de red profesional', 69.99, 1, 2, 'https://www.mcgillmicrowave.com/wp-content/uploads/2021/06/cat-6-ethernet-cable.webp'),
('Tarjeta de Red Gigabit', 'Adaptador PCI Express', 24.99, 1, 1, 'https://acosa.com.sv/wp-content/uploads/2025/05/TG-3468UN.webp'),

-- CATEGORÍA: COMPUTADORAS (CategoriaId = 2)
('Laptop Dell XPS 13', 'Laptop ultradelgada', 999.99, 2, 1, 'https://i5.walmartimages.com/seo/Dell-XPS-13-9380-Touchscreen-Laptop-13-3-Intel-Core-i7-8565U-8GB-RAM-256GB-SSD-Intel-UHD-Graphics-620-XPS9380-7660SLV-PUS_dc8dd562-496c-450a-aafe-0fce69c45922_1.c50e6906569d22b7c6498e583532854d.jpeg'),
('MacBook Air M2', 'Laptop Apple portátil', 1199.99, 2, 2, 'https://m.media-amazon.com/images/I/719C6bJv8jL._AC_SY355_.jpg'),
('PC Gamer Ryzen 7', 'Equipo para gaming', 1299.99, 2, 1, 'https://www.lacuracaonline.com/media/catalog/product/4/7/473917800016_1.jpg?optimize=medium&bg-color=255,255,255&fit=bounds&height=&width=&canvas=:'),
('Mini PC Intel NUC', 'Computadora compacta', 499.99, 2, 1, 'https://i5.walmartimages.com/seo/Intel-NUC-8-Mainstream-G-NUC8i5INH-Desktop-Computer-Intel-Core-i5-8265U-8GB-RAM-AMD-Radeon-540X-Windows-10-Home_ea8f50c6-1ee1-490e-b81b-2c60c38b81c0_1.5d4ba56377cb53c8e84566bbf5defc00.jpeg'),
('Teclado Logitech G512', 'Teclado mecánico RGB', 129.99, 2, 2, 'https://api.zonadigitalsv.com/storage/products/imagen_generada6a44210b02af8.jpg'),
('Mouse Razer DeathAdder', 'Mouse gamer ergonómico', 79.99, 2, 1, 'https://api.zonadigitalsv.com/storage/products/imagen_generada64750c4a4d6cd.jpg'),

-- CATEGORÍA: TELEFONÍA (CategoriaId = 3)
('iPhone 15', 'Smartphone Apple', 999.99, 3, 1, 'https://cdsassets.apple.com/live/7WUAS350/images/tech-specs/iphone_15_hero.png'),
('Samsung Galaxy S24', 'Teléfono Android premium', 899.99, 3, 2, 'https://www.radioshackla.com/media/catalog/product/4/6/465352700013.jpg?optimize=medium&bg-color=255,255,255&fit=bounds&height=&width=&canvas=:'),
('Google Pixel 8', 'Smartphone con IA', 799.99, 3, 1, 'https://media.wired.com/photos/6526b988c55060c7594b0d40/master/w_960,c_limit/Google-Pixel-8-Pro-and-Pixel-8-Review-Gear.jpg'),
('Xiaomi Redmi Note 13', 'Excelente rendimiento', 299.99, 3, 1, 'https://cdn.smart-gsm.com/blog/wp-content/uploads/2023/09/redmi-note-13-1.jpeg'),
('Motorola Edge', 'Pantalla OLED', 549.99, 3, 2, 'https://m.media-amazon.com/images/I/712nxo9u15L._AC_SX425_.jpg'),
('Samsung Galaxy A55', 'Gama media avanzada', 399.99, 3, 1, 'https://m.media-amazon.com/images/I/61b39S5DVtL._AC_SX569_.jpg'),

-- CATEGORÍA: AUDÍFONOS (CategoriaId = 4)
('Audífonos Tactiles Select Sound', 'Auriculares bluetooth con estuche inteligente interactivo', 34.99, 4, 1, 'https://selectsound.com.mx/cdn/shop/files/audifonos-inalambricos-bluetooth-pantalla-touch-modelo-ss-play-select-sound-bth050-321429.jpg?v=1741968264&width=1000'),

-- CATEGORÍA: MONITORES (CategoriaId = 5) - Línea corregida (Comillas removidas en SQL)
('Monitor Gaming AOC 27 pulgadas', 'Pantalla FHD de 27 pulgadas a 144Hz ideal para juegos competitivos', 199.99, 5, 1, 'https://m.media-amazon.com/images/I/71fUnwpI0ZL._AC_SY300_SX300_QL70_FMwebp_.jpg');
GO

-- 4. CONSULTA DE VERIFICACIÓN
SELECT P.[Id], P.[Nombre], P.[Precio], C.[Nombre] AS [Categoria], P.[Estado]
FROM [Productos] P
INNER JOIN [Categorias] C ON P.[CategoriaId] = C.[Id];
GO
*/