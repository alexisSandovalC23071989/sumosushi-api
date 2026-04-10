using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SumoSushiApp_BackEnd.DTOs.Producto;
using SumoSushiApp_BackEnd.Services;
using SumoSushiApp_BackEnd.Services.Interfaces;

namespace SumoSushiApp_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;
        private readonly IProductoService _productoService;
        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpGet]
        [Route("ObtenerVersionMenu")]
        public async Task<IActionResult> ObtenerVersionMenu()
        {            
            return Ok(await _menuService.ObtenerVersion());
        }

        // ===============================================

        [HttpGet]
        [Route("MenuCompleto")]
        public async Task<IActionResult> ObtenerMenuCompleto()
        {
            return Ok(await _menuService.ObtenerMenuCompleto());
        }


    }
}
