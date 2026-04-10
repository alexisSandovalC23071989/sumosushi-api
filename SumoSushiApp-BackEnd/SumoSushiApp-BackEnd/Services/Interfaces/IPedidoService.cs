using SumoSushiApp_BackEnd.DTOs.Pedido;
using SumoSushiApp_BackEnd.Models;

namespace SumoSushiApp_BackEnd.Services.Interfaces
{
    public interface IPedidoService
    {
        public Task<RespuestaModel> RealizarPedido(NuevoPedidoDTO model);
        public Task<RespuestaModel> ObtenerPedidos();
        public Task<RespuestaModel> ActualizarEstadoPedido(ActualizarEstadoPedidoDTO model);
        public Task<RespuestaModel> ObtenerPedidosPorUsuario(ObtenerPedidosUsuarioDTO model);
    }
}
