using System.Collections.Generic;

namespace SumoSushiApp_BackEnd.Models
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string ImagenUrl { get; set; }
        public bool CanjeablePuntos { get; set; }

        public int IdCategoria { get; set; }
        
        //public CategoriaProducto Categoria { get; set; }
        //public ICollection<EtiquetaProducto> EtiquetasProductos { get; set; }
        //public ICollection<CalificacionProducto> Calificaciones { get; set; }
    }
}