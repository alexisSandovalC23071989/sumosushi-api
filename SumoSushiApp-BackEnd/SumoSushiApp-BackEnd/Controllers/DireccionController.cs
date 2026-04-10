using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SumoSushiApp_BackEnd.Controllers.Utils;
using SumoSushiApp_BackEnd.DTOs.Direccion;
using SumoSushiApp_BackEnd.Services;
using SumoSushiApp_BackEnd.Services.Interfaces;

namespace SumoSushiApp_BackEnd.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DireccionController : ControllerBase
    {
        private readonly IDireccionService _direccionService;

        public DireccionController(IDireccionService direccionService)
        {
            _direccionService = direccionService;
        }

        /// Obtener todas las direcciones (activas e inactivas) de un usuario autenticado.
        [HttpGet]
        [Authorize]
        [Route("ObtenerDirecciones")]
        public async Task<IActionResult> ObtenerDirecciones()
        {

            var idUsuario = ControllerUtils.ObtenerUsuarioId(HttpContext);

            if (idUsuario <= 0)
            {
                return BadRequest("ID de usuario no válido o no autenticado.");
            }

            var respuesta = await _direccionService.ObtenerDireccionesUsuario(idUsuario);

            return Ok(respuesta);
        }

        /// Agregar una nueva dirección.
        [HttpPost]
        [Authorize]
        [Route("RegistrarDireccion")]
        public async Task<IActionResult> RegistrarDireccion([FromBody] DireccionCrearDTO dto)
        {
            var idUsuario = ControllerUtils.ObtenerUsuarioId(HttpContext);

            if (idUsuario <= 0)
                return BadRequest("ID de usuario no válido o no autenticado.");

            var respuesta = await _direccionService.AgregarDireccion(idUsuario, dto);

            return Ok(respuesta);
        }

        /// Actualizar (reemplazar) una dirección existente.
        [HttpPut]
        [Authorize]
        [Route("Actualizar")]
        public async Task<IActionResult> Actualizar([FromBody] DireccionActualizarDTO dto)
        {
            var idUsuario = ControllerUtils.ObtenerUsuarioId(HttpContext);

            if (idUsuario <= 0)
            {
                return BadRequest("ID de usuario no válido o no autenticado.");
            }

            var respuesta = await _direccionService.ActualizarDireccion(idUsuario, dto);

            return Ok(respuesta);
        }

        /// Deshabilitar (inactivar) una dirección existente.
        [HttpDelete]
        [Authorize]
        [Route("Deshabilitar")]
        public async Task<IActionResult> Deshabilitar([FromBody] DireccionDeshabilitarDTO dto)
        {
            var idUsuario = ControllerUtils.ObtenerUsuarioId(HttpContext);

            if (idUsuario <= 0)
            {
                return BadRequest("ID de usuario no válido o no autenticado.");
            }

            var respuesta = await _direccionService.DeshabilitarDireccion(idUsuario, dto.IdDireccion);

            return Ok(respuesta);
        }

        [HttpGet]
        [Authorize]
        [Route("GetCantones")]
        public async Task<IActionResult> GetCantones()
        {
            var resp = await _direccionService.ObtenerCantones();
            return Ok(resp);
        }

        [HttpGet]
        [Authorize]
        [Route("GetDistritos")]
        public async Task<IActionResult> GetDistritos()
        {
            var resp = await _direccionService.ObtenerDistritos();
            return Ok(resp);
        }

        //[HttpPost]
        //[Authorize]
        //[Route("distritos")]
        //public async Task<IActionResult> GetDistritos([FromBody] DistritosRequestDTO request)
        //{
        //    if (request == null || request.IdCanton <= 0)
        //        return BadRequest(new { Exito = false, Mensaje = "IdCanton es requerido y debe ser mayor que cero." });

        //    var resp = await _direccionService.ObtenerDistritos(request.IdCanton);
        //    return Ok(resp);
        //}
    }
}