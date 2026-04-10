namespace SumoSushiApp_BackEnd.DTOs.Producto
{
    public class ActualizarPresentacionProductoDTO
    {
        public long IdPresentacionProducto { get; set; }
        public long IdProducto { get; set; }
        public string? DetallePresentacion { get; set; }
        public double Precio { get; set; }
    }
}
