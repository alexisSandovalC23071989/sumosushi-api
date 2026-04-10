using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SumoSushiApp_BackEnd.Controllers.Utils;
using SumoSushiApp_BackEnd.DTOs.Categoria;
using SumoSushiApp_BackEnd.DTOs.Direccion;
using SumoSushiApp_BackEnd.Services;
using SumoSushiApp_BackEnd.Services.Interfaces;

namespace SumoSushiApp_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _service;


        public CategoriaController(ICategoriaService service)
        {
            _service = service;
        }
        //Obtener categorias
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("ObtenerCategorias")]
        public async Task<IActionResult> ObtenerCategorias()
        {
            // Validamos que el usuario este autenticado
            
            var respuesta = await _service.ObtenerCategorias();
            return Ok(respuesta);
        }
        //Obtener categorias activas
        [HttpGet("ObtenerActivas")]
        public async Task<IActionResult> ObtenerCategoriasActivas()
        {
            var respuesta = await _service.ObtenerCategoriasActivas();
            return Ok(respuesta);
        }
        //Obtener categorias inactivas
        [HttpGet("ObtenerInactivas")]
        public async Task<IActionResult> ObtenerCategoriasInactivas()
        {
            var respuesta = await _service.ObtenerCategoriasInactivas();
            return Ok(respuesta);
        }

        //Agregar Categorias
        [HttpPost]
        [Authorize]
        [Route("AgregarCategoria")]
        public async Task<IActionResult> AgregarCategoria([FromBody] CategoriaCrearDTO dto)
        {
            if (dto is null || string.IsNullOrWhiteSpace(dto.Descripcion))
            {
                return BadRequest("La descripción de la categoría es obligatoria.");
            }

            var respuesta = await _service.CrearCategoria(dto);

            return Ok(respuesta);
        }

        // Deshabilitar categoria 

        [HttpPut("DeshabilitarCategoria")]
        public async Task<IActionResult> DeshabilitarCategoria([FromBody] CategoriaDeshabilitarDTO dto)
        {
            var respuesta = await _service.DeshabilitarCategoria(dto.IdCategoria);
            return Ok(respuesta);
        }

        ///Habilitar Categoria

        [HttpPut("RehabilitarCategoria")]
        public async Task<IActionResult> RehabilitarCategoria([FromBody] CategoriaHabilitarDTO dto)
        {
            var respuesta = await _service.HabilitarCategoria(dto.IdCategoria);
            return Ok(respuesta);
        }


        //Actualizar Categoria

        [HttpPut("ActualizarCategoria")]
        public async Task<IActionResult> ActualizarCategoria([FromBody] CategoriaActualizarDTO dto)
        {
            var respuesta = await _service.ActualizarCategoria(dto);
            return Ok(respuesta);
        }


    }
}
