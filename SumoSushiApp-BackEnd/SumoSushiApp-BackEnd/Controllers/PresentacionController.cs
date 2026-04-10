using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SumoSushiApp_BackEnd.Services.Interfaces;

namespace SumoSushiApp_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PresentacionController : ControllerBase
    {
        private readonly IPresentacionService _presentacionService;
        public PresentacionController(IPresentacionService presentacionService)
        {
            _presentacionService = presentacionService;
        }

        [HttpGet("ObtenerPresentaciones")]
        public async Task<IActionResult> ObtenerPresentaciones()
        {
            var respuesta = await _presentacionService.ObtenerPresentaciones();
            return Ok(respuesta);
        }
    }
}
