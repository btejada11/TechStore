
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using TechStore.Data;
using TechStore.Models;
using TechStore.Services;
using Xunit;

namespace TechStore.Tests.IntegrationTests;

public class ProductoServiceTests
{
    private async Task<(SqliteConnection, ApplicationDbContext)>
        CrearContextoAsync()
    {
        var conexion = new SqliteConnection(
            "Data Source=:memory:");

        await conexion.OpenAsync();

        var opciones =
            new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlite(conexion)
                .Options;

        var contexto = new ApplicationDbContext(opciones);

        await contexto.Database.EnsureCreatedAsync();

        return (conexion, contexto);
    }

    // Crea una categoría válida para relacionar productos.
    private async Task<Categoria> CrearCategoriaAsync(
        ApplicationDbContext contexto)
    {
        var categoria = new Categoria
        {
            Nombre = "Computadoras",
            Descripcion = "Productos tecnológicos",
            Imagen = "categoria.jpg"
        };

        contexto.Categorias.Add(categoria);
        await contexto.SaveChangesAsync();

        return categoria;
    }

    private Producto CrearProducto(int categoriaId)
    {
        return new Producto
        {
            Nombre = "Laptop Dell",
            Descripcion = "Producto de prueba",
            Precio = 850.00m,
            Estado = Estado.Nuevo,
            Imagen = "laptop.jpg",
            CategoriaId = categoriaId,
            Stock = 5
        };
    }

    [Fact]
    public async Task ObtenerProductos_DebeRetornarListado()
    {
        var (conexion, contexto) = await CrearContextoAsync();
        using (conexion)
        await using (contexto)
        {
            var categoria = await CrearCategoriaAsync(contexto);
            var servicio = new TechStoreService(contexto);

            await servicio.GuardarProductoAsync(
                CrearProducto(categoria.Id));

            var productos = await servicio.ObtenerProductosAsync();

            Assert.Single(productos);
            Assert.Equal("Laptop Dell", productos.First().Nombre);
            Assert.NotNull(productos.First().Categoria);
        }
    }

    [Fact]
    public async Task ObtenerProductoPorId_DebeRetornarProducto()
    {
        var (conexion, contexto) = await CrearContextoAsync();
        using (conexion)
        await using (contexto)
        {
            var categoria = await CrearCategoriaAsync(contexto);
            var servicio = new TechStoreService(contexto);

            var producto = CrearProducto(categoria.Id);
            await servicio.GuardarProductoAsync(producto);

            var resultado =
                await servicio.ObtenerProductoPorIdAsync(producto.Id);

            Assert.NotNull(resultado);
            Assert.Equal(producto.Id, resultado.Id);
            Assert.Equal("Laptop Dell", resultado.Nombre);
        }
    }

    [Fact]
    public async Task GuardarProducto_DebeRegistrarProducto()
    {
        var (conexion, contexto) = await CrearContextoAsync();
        using (conexion)
        await using (contexto)
        {
            var categoria = await CrearCategoriaAsync(contexto);
            var servicio = new TechStoreService(contexto);

            var producto = CrearProducto(categoria.Id);

            await servicio.GuardarProductoAsync(producto);

            var guardado = await contexto.Productos
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == producto.Id);

            Assert.NotNull(guardado);
            Assert.True(guardado.Id > 0);
            Assert.Equal(850.00m, guardado.Precio);
            Assert.Equal(5, guardado.Stock);
        }
    }

    [Fact]
    public async Task GuardarProducto_DebeActualizarProducto()
    {
        var (conexion, contexto) = await CrearContextoAsync();
        using (conexion)
        await using (contexto)
        {
            var categoria = await CrearCategoriaAsync(contexto);
            var servicio = new TechStoreService(contexto);

            var producto = CrearProducto(categoria.Id);
            await servicio.GuardarProductoAsync(producto);

            // Modificar el producto existente.
            producto.Nombre = "Laptop Dell Actualizada";
            producto.Precio = 950.00m;

            await servicio.GuardarProductoAsync(producto);

            var actualizado = await contexto.Productos
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == producto.Id);

            Assert.NotNull(actualizado);
            Assert.Equal("Laptop Dell Actualizada",
                actualizado.Nombre);
            Assert.Equal(950.00m, actualizado.Precio);
        }
    }

    [Fact]
    public async Task EliminarProducto_DebeEliminarRegistro()
    {
        var (conexion, contexto) = await CrearContextoAsync();
        using (conexion)
        await using (contexto)
        {
            var categoria = await CrearCategoriaAsync(contexto);
            var servicio = new TechStoreService(contexto);

            var producto = CrearProducto(categoria.Id);
            await servicio.GuardarProductoAsync(producto);

            await servicio.EliminarProductoAsync(producto.Id);

            var eliminado = await contexto.Productos
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == producto.Id);

            Assert.Null(eliminado);
        }
    }
}
