namespace SumoSushiApp_BackEnd.DTOs.Pedido
{
    public class PedidoModel
    {
        public long IdPedido { get; set; }
        public DateTime FechaCreacion { get; set; }
        public decimal CostoEnvio { get; set; }
        public int PuntosCanjeados { get; set; }
        public decimal TotalConPuntos { get; set; }
        public decimal TotalPagado { get; set; }
        public string? MetodoPago { get; set; }
        public string? TipoPedido { get; set; }
        public string? NotaCliente { get; set; }
        public int IdEstadoPedido { get; set; }
        public string? EstadoPedido { get; set; }
        public long IdUsuario { get; set; }
        public string? Nombre { get; set; }
        public string? Apellidos { get; set; }
        public string? Telefonos { get; set; }
        public string? Direccion { get; set; }

        public IEnumerable<PromocionModel>? Promociones { get; set; }
        public IEnumerable<ProductoModel>? Productos { get; set; } 
    }
}
