using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SumoSushiApp_BackEnd.Services.Interfaces;

namespace SumoSushiApp_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EtiquetaController : ControllerBase
    {
        private readonly IEtiquetaService _etiquetaService;
        public EtiquetaController(IEtiquetaService etiquetaService)
        {
            _etiquetaService = etiquetaService;
        }

        [HttpGet]
        [Route("ObtenerEtiquetas")]
        public async Task<IActionResult> ObtenerEtiquetas()
        {
            var respuesta = await _etiquetaService.ObtenerEtiquetas();

            return Ok(respuesta);
        }
            
    }
}

