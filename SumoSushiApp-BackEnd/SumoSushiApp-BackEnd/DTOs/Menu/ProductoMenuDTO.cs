using System.Collections.Generic;

namespace SumoSushiApp_BackEnd.DTOs.Menu
{
    public class ProductoMenuDTO
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public string Descripcion { get; set; }
        public string ImagenUrl { get; set; }
        public List<string> Etiquetas { get; set; }
        public string Categoria { get; set; }
        public bool EsPromocion { get; set; }
    }
}