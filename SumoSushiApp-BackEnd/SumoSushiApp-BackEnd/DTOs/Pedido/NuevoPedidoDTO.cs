using System.ComponentModel.DataAnnotations;

namespace SumoSushiApp_BackEnd.DTOs.Pedido
{
    public class NuevoPedidoDTO
    {        
        public long IdUsuario { get; set; }
        [Required]
        public int IdTipoPedido { get; set; }
        [Required]
        public int IdMetodoPago { get; set; }
        public int PuntosCanjeados { get; set; }
        public string? NotaCliente { get; set; }
        public long? IdDireccion { get; set; }    
        
    }
}
