using SumoSushiApp_BackEnd.DTOs.Pedido;

namespace SumoSushiApp_BackEnd.Repositories.Interfaces
{
    public interface IPedidoRepository
    {
        public Task<(int codigoError, string mensaje)> RegistrarPedido(NuevoPedidoDTO model);
        public Task<IEnumerable<PedidoModel>> ObtenerPedidosAsync();
        public Task<(int codigoError, string mensaje)> ActualizarEstadoPedido(ActualizarEstadoPedidoDTO model);
        public Task<IEnumerable<PedidoModel>> ObtenerPedidosPorUsuarioAsync(ObtenerPedidosUsuarioDTO model);
        public Task<InfoActualizarPedidoDTO?> ObtenerInfoActualizarPedido(ActualizarEstadoPedidoDTO model);
    }
}
