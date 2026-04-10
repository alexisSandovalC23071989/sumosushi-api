using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SumoSushiApp_BackEnd.Controllers.Utils;
using SumoSushiApp_BackEnd.DTOs.Puntos;
using SumoSushiApp_BackEnd.Services.Interfaces;

namespace SumoSushiApp_BackEnd.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PuntosController : ControllerBase
    {
        private readonly IPuntosService _service;
        public PuntosController(IPuntosService service) => _service = service;

        [HttpGet]
        [Route("ConsultarPuntos")]
        public async Task<IActionResult> ConsultarPuntos()
        {
            var idUsuario = ControllerUtils.ObtenerUsuarioId(HttpContext);
            var resp = await _service.ObtenerPuntosUsuario(idUsuario);
            return Ok(resp);
        }

        [HttpPost]
        [Route("CanjearPuntos")]
        public async Task<IActionResult> CanjearPuntos([FromBody] CanjearPuntosRequestDTO dto)
        {
            var idUsuario = ControllerUtils.ObtenerUsuarioId(HttpContext);
            var resp = await _service.CanjearPuntosUsuario(idUsuario, dto.PuntosACanjear);
            return Ok(resp);
        }
    }
}