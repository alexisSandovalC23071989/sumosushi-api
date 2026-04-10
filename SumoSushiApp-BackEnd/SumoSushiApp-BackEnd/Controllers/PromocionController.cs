using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SumoSushiApp_BackEnd.DTOs.Promocion;
using SumoSushiApp_BackEnd.Services.Interfaces;

namespace SumoSushiApp_BackEnd.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class PromocionController : ControllerBase
    {
        private readonly IPromocionService _promocionService;
        public PromocionController(IPromocionService promocionService) => _promocionService = promocionService;

        [HttpGet]
        [Route("ObtenerPromociones")]
        public async Task<IActionResult> Todas()
        {
            var resp = await _promocionService.ObtenerTodasPromociones();
            return Ok(resp);
        }

        [HttpGet]
        [Route("PromocionesActivas")]
        public async Task<IActionResult> Activas()
        {
            var resp = await _promocionService.ObtenerPromocionesActivas();
            return Ok(resp);
        }

        [HttpGet]
        [Route("PromocionesInactivas")]
        public async Task<IActionResult> Inactivas()
        {
            var resp = await _promocionService.ObtenerPromocionesInactivas();
            return Ok(resp);
        }

        [HttpPost]
        [Route("CrearPromocion")]
        public async Task<IActionResult> Crear([FromForm] PromocionCrearDTO dto)
        {
            var resp = await _promocionService.CrearPromocion(dto);
            return Ok(resp);
        }

        [HttpPut]
        [Route("ActualizarPromocion")]
        public async Task<IActionResult> Editar([FromForm] PromocionActualizarDTO dto)
        {
            var resp = await _promocionService.ActualizarPromocion(dto);
            return Ok(resp);
        }

        [HttpPut]
        [Route("ActivarPromocion")]
        public async Task<IActionResult> Activar([FromBody] PromocionCambiarEstadoDTO dto)
        {
            var resp = await _promocionService.ActivarPromocion(dto);
            return Ok(resp);
        }

        [HttpPut]
        [Route("DeshabilitarPromocion")]
        public async Task<IActionResult> Deshabilitar([FromBody] PromocionCambiarEstadoDTO dto)
        {
            var resp = await _promocionService.DeshabilitarPromocion(dto);
            return Ok(resp);
        }
    }
}