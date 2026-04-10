using SumoSushiApp_BackEnd.DTOs.Etiqueta;
using SumoSushiApp_BackEnd.DTOs.Producto;

namespace SumoSushiApp_BackEnd.DTOs.Menu
{
    public class ProductosTotalesDTO
    {
        public long IdProducto { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public string? ImagenUrl { get; set; }
        public bool CanjeablePuntos { get; set; }
        public long IdCategoria { get; set; }
        public string? Categoria { get; set; }
        public string? Estado { get; set; }
        public IEnumerable<EtiquetasProductosDTO>? Etiquetas { get; set; }
        public IEnumerable<PresentacionProductoDTO>? Presentaciones { get; set; }
    }
}
