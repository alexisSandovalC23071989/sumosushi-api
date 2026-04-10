using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SumoSushiApp_BackEnd.Controllers.Utils;
using SumoSushiApp_BackEnd.DTOs.Usuario;
using SumoSushiApp_BackEnd.Services.Interfaces;

namespace SumoSushiApp_BackEnd.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TelefonoController : ControllerBase
    {
        private readonly ITelefonoService _telefonoService;
        public TelefonoController(ITelefonoService telefonoService)
        {
            _telefonoService = telefonoService;
        }

        [HttpPost]
        [Route("RegistrarTelefono")]
        public async Task<IActionResult> RegistrarTelefono(NuevoTelefonoUsuarioDTO model)
        {
            model.IdUsuario = ControllerUtils.ObtenerUsuarioId(HttpContext);
            var respuesta = await _telefonoService.RegistrarTelefono(model);
            return Ok(respuesta);
        }

        [HttpDelete]
        [Route("EliminarTelefono")]
        public async Task<IActionResult> eliminarTelefono(EliminarTelefonoUsuarioDTO model)
        {
            model.IdUsuario = ControllerUtils.ObtenerUsuarioId(HttpContext);
            var respuesta = await _telefonoService.EliminarTelefono(model);
            return Ok(respuesta);
        }

    }
}
