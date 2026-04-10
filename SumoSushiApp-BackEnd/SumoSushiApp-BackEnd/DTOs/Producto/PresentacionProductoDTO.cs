namespace SumoSushiApp_BackEnd.DTOs.Producto
{
    public class PresentacionProductoDTO
    {
        public long IdPresentacionProducto { get; set; }
        public long IdProducto { get; set; }
        public string? DetallePresentacion { get; set; }
        public double Precio { get; set; }
        public string? Estado { get; set; }
    }
}
