using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SumoSushiApp_BackEnd.DTOs.Horario;
using SumoSushiApp_BackEnd.Services.Interfaces;

namespace SumoSushiApp_BackEnd.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class HorarioController : ControllerBase
    {
        private readonly IHorarioService _horarioService;

        public HorarioController(IHorarioService horarioService)
        {
            _horarioService = horarioService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("ObtenerDias")]
        public async Task<IActionResult> ObtenerDias()
        {
            var respuesta = await _horarioService.ObtenerDias();
            return Ok(respuesta);
        }

        [AllowAnonymous]
        [HttpGet("ObtenerHorariosRestaurante")]
        public async Task<IActionResult> ObtenerHorariosRestaurante()
        {
            var respuesta = await _horarioService.ObtenerHorariosRestaurante();
            return Ok(respuesta);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("RegistrarHorario")]
        public async Task<IActionResult> RegistrarHorario(ActualizarHorarioDTO model)
        {
            if (model.HoraApertura >= model.HoraCierre)
            {
                return BadRequest("La hora de apertura debe ser anterior a la hora de cierre.");
            }
            var respuesta = await _horarioService.RegistrarHorario(model);
            return Ok(respuesta);
        }
    }
}
