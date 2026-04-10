using Microsoft.AspNetCore.Mvc;
using SumoSushiApp_BackEnd.DTOs.Menu;
using SumoSushiApp_BackEnd.DTOs.Producto;
using SumoSushiApp_BackEnd.Repositories;
using SumoSushiApp_BackEnd.Services;
using SumoSushiApp_BackEnd.Services.Interfaces;
using System.Text.Json;


namespace SumoSushiApp_BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : ControllerBase
    {
        #region Stheven
        //private readonly IProductoRepository _productoRepository;
        //private readonly IProductoService _productoService;

        //public ProductoController(IProductoRepository productoRepository, IProductoService productoService)
        //{
        //    _productoRepository = productoRepository;
        //    _productoService = productoService;
        //}

        //// =========================
        //// OBTENER MENÚ COMPLETO
        //// =========================
        //[HttpGet("Menu")]
        //public IActionResult ObtenerMenuCompleto()
        //{
        //    try
        //    {
        //        var productos = _productoRepository.ObtenerMenuCompleto();
        //        return Ok(productos);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { mensaje = ex.Message });
        //    }
        //}

        //// =========================
        //// OBTENER DETALLE
        //// =========================
        //[HttpPost("Detalle")]
        //public IActionResult ObtenerDetalle([FromBody] ProductoRequestDTO dto)
        //{
        //    try
        //    {
        //        var producto = _productoRepository.ObtenerDetalleProducto(dto.IdProducto);
        //        if (producto == null)
        //            return NotFound(new { mensaje = "Producto no encontrado" });

        //        return Ok(producto);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { mensaje = ex.Message });
        //    }
        //}

        //// =========================
        //// CALIFICAR PRODUCTO
        //// =========================
        //[HttpPost("Calificar")]
        //public IActionResult Calificar([FromBody] CalificarProductoRequestDTO dto)
        //{
        //    var usuarioId = ObtenerUsuarioId(); // Temporalmente simulado

        //    try
        //    {
        //        var (mensaje, codigoError) = _productoRepository.CalificarProducto(usuarioId, dto.IdProducto, dto.Calificacion);

        //        if (codigoError != 0)
        //            return BadRequest(new { mensaje });

        //        return Ok(new { mensaje });
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new { mensaje = ex.Message });
        //    }
        //}

        //private int ObtenerUsuarioId()
        //{
        //    return 9; // Temporal
        //}
        #endregion

        private readonly IMenuService _menuService;
        private readonly IProductoService _productoService;

        public ProductoController(IMenuService menuService, IProductoService productoService)
        {
            _menuService = menuService;
            _productoService = productoService;
        }

        [HttpGet]
        [Route("ObtenerProductosPresentacionesTotales")]
        public async Task<IActionResult> ObtenerProductosPresentacionesTotales()
        {
            return Ok(await _menuService.ObtenerProductosPresentacionesTotales());
        }

        [HttpPost("RegistrarProducto")]
        public async Task<IActionResult> RegistrarProductoCompleto([FromForm] ProductoFormDTO formDto)
        {
            // Deserializar el JSON recibido en el form
            var model = JsonSerializer.Deserialize<RegistroProductoDTO>(
                formDto.modelJson,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );

            if (model == null)
                return BadRequest("Error al deserializar el modelo");

            // Asignar la imagen al modelo
            model.ImagenFile = formDto.ImagenFile;

            // Validación manual si es necesario (opcional)
            if (model.Presentaciones == null || !model.Presentaciones.Any())
                return BadRequest("Debe incluir al menos una presentación.");

            return Ok(await _productoService.RegistrarProductoCompleto(model));
        }

        [HttpPut("ActualizarProducto")]
        public async Task<IActionResult> ActualizarProductoCompleto([FromForm] ProductoFormDTO formDto)
        {
            ActualizarProductoDTO model;

            model = ActualizarProductoDTO.FromFormDto(formDto);

            if (model.Presentaciones == null || !model.Presentaciones.Any())
                return BadRequest("Debe incluir al menos una presentación.");
            return Ok(await _productoService.ActualizarProductoCompleto(model));
        }
    }
}
