using TechStore.Models;

namespace TechStore.Services
{
    // Interfaz que define las operaciones CRUD para las categorías.
    // Aplicamos el principio de Inversión de Dependencias (SOLID):
    // El controlador dependerá de esta abstracción y no de la implementación concreta.
    public interface ICategoriaService
    {
        // Obtiene el listado completo de categorías registradas en la base de datos
        Task<IEnumerable<Categoria>> ObtenerTodasAsync();

        // Busca una categoría específica mediante su id (retorna null si no existe)
        Task<Categoria?> ObtenerPorIdAsync(int id);

        // Agrega una nueva categoría a la base de datos
        Task CrearAsync(Categoria categoria);

        // Actualiza los datos de una categoría existente
        Task ActualizarAsync(Categoria categoria);

        // Elimina una categoría de la base de datos según su ID
        // Retorna una tupla indicando si fue exitoso y el mensaje descriptivo del resultado
        Task<(bool Exito, string Mensaje)> EliminarAsync(int id);
    }
}