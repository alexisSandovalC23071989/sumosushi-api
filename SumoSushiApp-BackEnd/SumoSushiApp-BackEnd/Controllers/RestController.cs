using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SumoSushiApp_BackEnd.DTOs.Restaurante;
using SumoSushiApp_BackEnd.Services.Interfaces;

namespace SumoSushiApp_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestController : ControllerBase
    {
        private readonly IRestauranteService _restauranteService;

        public RestController(IRestauranteService restauranteService)
        {
            _restauranteService = restauranteService;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("ObtenerInfoRestaurante")]
        public async Task<IActionResult> GetInfoRestaurante()
        {
            var respuesta = await _restauranteService.ObtenerInfoRestaurante();
            return Ok(respuesta);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        [Route("ActualizarInfoRestaurante")]
        public async Task<IActionResult> ActualizarInfoRestaurante(InfoRestauranteDTO model)
        {
            var respuesta = await _restauranteService.ActualizarInfoRestaurante(model);
            return Ok(respuesta);
        }

    }
}
