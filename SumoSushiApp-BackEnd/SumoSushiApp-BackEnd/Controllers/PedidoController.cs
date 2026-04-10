using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SumoSushiApp_BackEnd.Controllers.Utils;
using SumoSushiApp_BackEnd.DTOs.Pedido;
using SumoSushiApp_BackEnd.Services.Interfaces;

namespace SumoSushiApp_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;
        public PedidoController(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService ?? throw new ArgumentNullException(nameof(pedidoService));
        }

        [Authorize]
        [HttpPost]
        [Route("RealizarPedido")]
        public async Task<IActionResult> RealizarPedido(NuevoPedidoDTO pedido)
        {
            pedido.IdUsuario = ControllerUtils.ObtenerUsuarioId(HttpContext);

            return Ok(await _pedidoService.RealizarPedido(pedido));

        }

        [Authorize(Roles = "Admin, Colaborador")]
        [HttpGet]
        [Route("ObtenerPedidos")]
        public async Task<IActionResult> ObtenerPedidos()
        {
            var respuesta = await _pedidoService.ObtenerPedidos();
            return Ok(respuesta);
        }

        [Authorize]
        [HttpPut]
        [Route("ActualizarEstadoPedido")]
        public async Task<IActionResult> ActualizarEstadoPedido(ActualizarEstadoPedidoDTO model)
        {
            return Ok(await _pedidoService.ActualizarEstadoPedido(model));
        }

        [Authorize]
        [HttpGet]
        [Route("ObtenerPedidosCliente")]
        public async Task<IActionResult> ObtenerPedidosCliente()
        {
            var model = new ObtenerPedidosUsuarioDTO();
            model.IdUsuario = ControllerUtils.ObtenerUsuarioId(HttpContext);
            return Ok(await _pedidoService.ObtenerPedidosPorUsuario(model));

        }
    }
}
