namespace SumoSushiApp_BackEnd.DTOs.Producto
{
    public class ActualizarEtiquetaProductoDTO
    {
        public long IdProducto { get; set; }
        public long IdEtiqueta { get; set; }
        public string? DetalleEtiqueta { get; set; }
    }
}
