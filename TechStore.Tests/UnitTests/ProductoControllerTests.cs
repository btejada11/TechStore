
using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TechStore.Controllers;
using TechStore.Models;
using TechStore.Services;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace TechStore.Tests.UnitTests
{
    public class ProductoControllerTests
    {


        private ProductoController CrearController(
            Mock<ITechStoreService> mockService,
            Mock<ILogger<ProductoController>> mockLogger)
        {
            var controller = new ProductoController(
                mockService.Object,
                mockLogger.Object
            );

            var usuario = new ClaimsPrincipal(
                new ClaimsIdentity(
                    new[]
                    {
                new Claim(ClaimTypes.Name, "UsuarioPrueba"),
                new Claim(ClaimTypes.Role, "Admin")
                    },
                    "TestAuthentication"
                )
            );

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = usuario
                }
            };

            return controller;
        }














        [Fact]
        public async Task Index_DebeMostrarProductos()
        {
            var productos = new List<Producto>
            {
                new Producto
                {
                    Id = 1,
                    Nombre = "Laptop",
                    Precio = 850,
                    Estado = Estado.Nuevo,
                    Imagen = "laptop.jpg",
                    CategoriaId = 1,
                    Stock = 5
                },
                new Producto
                {
                    Id = 2,
                    Nombre = "Mouse",
                    Precio = 25,
                    Estado = Estado.Nuevo,
                    Imagen = "mouse.jpg",
                    CategoriaId = 1,
                    Stock = 10
                }
            };

            var mockService = new Mock<ITechStoreService>();

            mockService
                .Setup(s => s.ObtenerProductosAsync())
                .ReturnsAsync(productos);

            var mockLogger =
                new Mock<ILogger<ProductoController>>();

            var controller = new ProductoController(
                mockService.Object,
                mockLogger.Object
            );

            var resultado = await controller.Index();

            var vista = Assert.IsType<ViewResult>(resultado);

            var modelo = Assert.IsAssignableFrom<
                IEnumerable<Producto>>(vista.Model);

            Assert.Equal(2, modelo.Count());

            mockService.Verify(
                s => s.ObtenerProductosAsync(),
                Times.Once
            );
        }



        [Fact]
        public async Task Edit_DebeObtenerProductoPorId()
        {
            // Arrange
            var producto = new Producto
            {
                Id = 1,
                Nombre = "Laptop",
                Precio = 850,
                CategoriaId = 1,
                Imagen = "laptop.jpg",
                Stock = 5
            };

            var mockService = new Mock<ITechStoreService>();
            var mockLogger = new Mock<ILogger<ProductoController>>();

            mockService
                .Setup(s => s.ObtenerProductoPorIdAsync(1))
                .ReturnsAsync(producto);

            var controller = new ProductoController(
                mockService.Object,
                mockLogger.Object);

            // Act
            var resultado = await controller.Edit(1);

            // Assert
            var vista = Assert.IsType<ViewResult>(resultado);
            var modelo = Assert.IsType<Producto>(vista.Model);

            Assert.Equal(1, modelo.Id);
            Assert.Equal("Laptop", modelo.Nombre);

            mockService.Verify(
                s => s.ObtenerProductoPorIdAsync(1),
                Times.Once);
        }

    

        [Fact]
        public async Task Create_DebeRegistrarProducto()
        {
            // Arrange
            var producto = new Producto
            {
                Nombre = "Mouse",
                Precio = 25,
                Estado = Estado.Nuevo,
                Imagen = "mouse.jpg",
                CategoriaId = 1,
                Stock = 10
            };

            var mockService = new Mock<ITechStoreService>();
            var mockLogger = new Mock<ILogger<ProductoController>>();

            var controller = CrearController(
                    mockService,
                     mockLogger);

            // Act
            var resultado = await controller.Create(producto);

            // Assert
            Assert.IsType<RedirectToActionResult>(resultado);

            mockService.Verify(
                s => s.GuardarProductoAsync(producto),
                Times.Once);
        }

        [Fact]
        public async Task Edit_DebeActualizarProducto()
        {
            // Arrange
            var producto = new Producto
            {
                Id = 1,
                Nombre = "Laptop Actualizada",
                Precio = 900,
                Estado = Estado.Nuevo,
                Imagen = "laptop.jpg",
                CategoriaId = 1,
                Stock = 8
            };

            var mockService = new Mock<ITechStoreService>();
            var mockLogger = new Mock<ILogger<ProductoController>>();

            var controller = CrearController(
                    mockService,
                   mockLogger);

            // Act
            var resultado = await controller.Edit(1, producto);

            // Assert
            Assert.IsType<RedirectToActionResult>(resultado);

            mockService.Verify(
                s => s.GuardarProductoAsync(producto),
                Times.Once);
        }

        [Fact]
        public async Task Delete_DebeEliminarProducto()
        {
            // Arrange
            var producto = new Producto
            {
                Id = 1,
                Nombre = "Laptop",
                Precio = 850,
                Estado = Estado.Nuevo,
                Imagen = "laptop.jpg",
                CategoriaId = 1,
                Stock = 5
            };

            var mockService = new Mock<ITechStoreService>();
            var mockLogger = new Mock<ILogger<ProductoController>>();

            mockService
                .Setup(s => s.ObtenerProductoPorIdAsync(1))
                .ReturnsAsync(producto);

            var controller = CrearController(
              mockService,
                 mockLogger);

            // TempData necesario para DeleteConfirmed
            controller.TempData =
                new Microsoft.AspNetCore.Mvc.ViewFeatures.TempDataDictionary(
                    new Microsoft.AspNetCore.Http.DefaultHttpContext(),
                    Mock.Of<
                        Microsoft.AspNetCore.Mvc.ViewFeatures.ITempDataProvider>());

            // Act
            var resultado = await controller.DeleteConfirmed(1);

            // Assert
            Assert.IsType<RedirectToActionResult>(resultado);

            mockService.Verify(
                s => s.EliminarProductoAsync(1),
                Times.Once);
        }
    } }
