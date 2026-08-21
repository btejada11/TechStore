using System.Data.Common;

namespace TechStore.Models
{
   /// <summary>
   /// 
   /// </summary>
   

    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public Models.Categorias Categoria { get; set; }
        public Models.Estado Estado { get; set; }

        public string Imagen { get; set; } = string.Empty;


    }

 

}
