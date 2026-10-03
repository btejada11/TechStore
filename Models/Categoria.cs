// Models/Categoria.cs
using System.ComponentModel.DataAnnotations;

namespace TechStore.Models
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de la categoría es obligatorio.")]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [StringLength(500)]
        public string Descripcion { get; set; } = string.Empty;

        [Required(ErrorMessage = "La URL de la imagen es obligatoria.")]
        public string Imagen { get; set; } = string.Empty;

        // Relación: Una categoría tiene muchos productos
        public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
    }
}
