using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SumoSushiApp_BackEnd.Controllers.Utils;
using SumoSushiApp_BackEnd.DTOs.Carrito;
using SumoSushiApp_BackEnd.Services.Interfaces;

namespace SumoSushiApp_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarritoController : ControllerBase
    {
        private readonly ICarritoService _service;

        public CarritoController(ICarritoService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpPost("Agregar")]
        public async Task<IActionResult> AgregarProducto([FromBody] CarritoAgregarDTO dto)
        {
            dto.IdUsuario = ControllerUtils.ObtenerUsuarioId(HttpContext);

            if (dto.IdPresentacionProducto == 0)
                dto.IdPresentacionProducto = null;
            if (dto.IdPromocion == 0)
                dto.IdPromocion = null;

            bool tienePresentacion = dto.IdPresentacionProducto.HasValue;
            bool tienePromocion = dto.IdPromocion.HasValue;

            if (tienePresentacion == tienePromocion) // ambos true o ambos false
            {
                return BadRequest(new
                {
                    exito = false,
                    mensaje = "Debe enviar solo uno entre IdPresentacionProducto o IdPromocion (no ambos o ninguno).",
                    datos = (object?)null,
                    alerta = 0
                });
            }

            var respuesta = await _service.AgregarOActualizarProducto(dto);
            return Ok(respuesta);
        }

        [Authorize]
        [HttpGet("Obtener")]
        public async Task<IActionResult> ObtenerCarrito()
        {
            var idUsuario = ControllerUtils.ObtenerUsuarioId(HttpContext);
            var respuesta = await _service.ObtenerCarrito(idUsuario);
            return Ok(respuesta);
        }
    }
}

