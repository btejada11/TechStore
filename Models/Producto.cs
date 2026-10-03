// Models/Producto.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechStore.Models
{
    // Definición del Enum directamente en el contexto de Producto
    public enum Estado
    {
        Nuevo = 1,
        Open_Box = 2
    }

    public class Producto
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del producto es obligatorio.")]
        [StringLength(150)]
        public string Nombre { get; set; } = string.Empty;

        [StringLength(500)]
        public string Descripcion { get; set; } = string.Empty;

        [Required(ErrorMessage = "El precio es obligatorio.")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Precio { get; set; }

        [Required(ErrorMessage = "El estado es obligatorio.")]
        public Estado Estado { get; set; } // Uso del Enum local

        [Required(ErrorMessage = "La URL de la imagen es obligatoria.")]
        public string Imagen { get; set; } = string.Empty;

        // Clave Foránea hacia Categoria
        [Required(ErrorMessage = "La categoría es obligatoria.")]
        public int CategoriaId { get; set; }

        // Propiedad de navegación relacional
        [ForeignKey("CategoriaId")]
        public virtual Categoria? Categoria { get; set; }
        [Required(ErrorMessage = "El stock es obligatorio.")]
        [Range(0, int.MaxValue, ErrorMessage = "El stock no puede ser negativo.")]
        public int Stock { get; set; }
    }
}

