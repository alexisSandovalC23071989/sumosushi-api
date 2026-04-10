using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SumoSushiApp_BackEnd.Services.Interfaces;

namespace SumoSushiApp_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        private readonly IRolService _rolService;
        public RolController(IRolService rolService)
        {
            _rolService = rolService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("ObtenerRoles")]
        public async Task<IActionResult> ObtenerRoles()
        {
            var respuesta = await _rolService.ObtenerRoles();
            return Ok(respuesta);
        }
    }
}
