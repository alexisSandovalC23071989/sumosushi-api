
using Microsoft.AspNetCore.SignalR;
using MySql.Data.MySqlClient;
using SumoSushiApp_BackEnd.Data;
using SumoSushiApp_BackEnd.DTOs.Pedido;
using SumoSushiApp_BackEnd.Hubs;
using SumoSushiApp_BackEnd.Models;
using SumoSushiApp_BackEnd.Repositories.Interfaces;
using SumoSushiApp_BackEnd.Services.Interfaces;
using SumoSushiApp_BackEnd.Services.Utils;
using System.Security.Policy;

namespace SumoSushiApp_BackEnd.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IDBContext _dbContext;
        private readonly IHubContext<PedidoHub> _hubContext;
        private readonly IPushNotificationService _pushNotificationService;

        public PedidoService(IPedidoRepository pedidoRepository, IDBContext dbContext, IHubContext<PedidoHub> hubContext, IPushNotificationService pushNotificationService)
        {
            _pedidoRepository = pedidoRepository ?? throw new ArgumentNullException(nameof(pedidoRepository));
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));
            _pushNotificationService = pushNotificationService;
        }

        public async Task<RespuestaModel> RealizarPedido(NuevoPedidoDTO model)
        {
            using var conexion = _dbContext.MySqlConexion();

            var (codigoError, mensaje) = await _pedidoRepository.RegistrarPedido(model);
            if (codigoError != 0)
                return RespuestasUtils.GenerarRespuesta(codigoError, mensaje);

            var mensajeHub = new MensajeHubModel(mensaje);

            // AVISAR AL HUB
            await _hubContext.Clients.All.SendAsync("RecibirPedido", mensajeHub);

            // ENVIAR CORREO A USUARIO
            // ...

            return RespuestasUtils.GenerarRespuesta(codigoError, mensaje);
        }

        public async Task<RespuestaModel> ObtenerPedidos()
        {
            //using var conexion = _dbContext.MySqlConexion();
            return RespuestasUtils.GenerarRespuesta(await _pedidoRepository.ObtenerPedidosAsync());
        }

        public async Task<RespuestaModel> ObtenerPedidosPorUsuario(ObtenerPedidosUsuarioDTO model)
        {
            //using var conexion = _dbContext.MySqlConexion();
            return RespuestasUtils.GenerarRespuesta(await _pedidoRepository.ObtenerPedidosPorUsuarioAsync(model));
        }

        public async Task<RespuestaModel> ActualizarEstadoPedido(ActualizarEstadoPedidoDTO model)
        {            
            var (codigoError, mensaje) = await _pedidoRepository.ActualizarEstadoPedido(model);
            if (codigoError != 0)
                return RespuestasUtils.GenerarRespuesta(codigoError, mensaje);

            // INFORMACION DEL CLIENTE 
            var infoActualizacion = await _pedidoRepository.ObtenerInfoActualizarPedido(model);
            // NOTIFICAR A EL CLIENTE
            if (infoActualizacion is not null)
                _pushNotificationService.EnviarNotificacionAUsuarioAsync(
                    infoActualizacion.IdUsuario, 
                    "Actualización del pedido " + model.IdPedido, 
                    "Su pedido ha pasado a: " + infoActualizacion.EstadoPedido, 
                    "/#/seguimientoPedido").Wait();


            return RespuestasUtils.GenerarRespuesta(codigoError, mensaje);
        }
    }
}
