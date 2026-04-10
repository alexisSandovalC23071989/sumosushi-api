using System.ComponentModel.DataAnnotations;

namespace SumoSushiApp_BackEnd.DTOs.Pedido
{
    public class ActualizarEstadoPedidoDTO
    {
        [Required]
        public long IdPedido { get; set; }
        [Required]
        public int IdEstadoPedido { get; set; }
    }
}
