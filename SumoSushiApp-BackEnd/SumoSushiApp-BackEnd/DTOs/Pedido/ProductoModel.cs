namespace SumoSushiApp_BackEnd.DTOs.Pedido
{
    public class ProductoModel
    {
        public long IdPedido { get; set; }
        public string? NombreProducto { get; set; }
        public string? Presentacion { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
    }
}
