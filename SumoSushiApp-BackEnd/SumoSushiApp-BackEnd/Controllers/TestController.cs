using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SumoSushiApp_BackEnd.Controllers.Utils;
using SumoSushiApp_BackEnd.Data;
using SumoSushiApp_BackEnd.Hubs;
using SumoSushiApp_BackEnd.Services.Interfaces;
using System.Security.Policy;

namespace SumoSushiApp_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IDBContext _context;
        private readonly ICorreoService _correoService;
        private readonly IImgService _imgService;
        private readonly IHubContext<PedidoHub> _hubContext;
        private readonly IPushNotificationService _pushNotificationService;

        public TestController(IDBContext context, ICorreoService correoService, IImgService imgService, IHubContext<PedidoHub> hubContext, IPushNotificationService pushNotificationService)
        {
            _context = context;
            _correoService = correoService;
            _imgService = imgService;
            _hubContext = hubContext;
            _pushNotificationService = pushNotificationService;
        }

        [HttpGet("Version")]
        public async Task<IActionResult> GetDbVersion()
        {
            try
            {
                using var connection = _context.MySqlConexion();
                var version = await connection.QuerySingleAsync<string>("SELECT VERSION();");
                return Ok(new { MySQLVersion = version });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet]
        [Authorize]
        [Route("TestAuthorize")]
        public IActionResult TestAuthorize()
        {
            return Ok(new { resultado = "Autenticación exitosa. El usuario está autorizado para acceder a este recurso." });
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Colaborador")]
        [Route("SoloParaAdmin")]
        public IActionResult GetSoloParaAdmin()
        {
            return Ok(new { resultado = "Acceso exclusivo para administradores" });
        }

        [HttpGet]
        [Authorize]
        [Route("Idusuario")]
        public IActionResult GetIdUsuario()
        {
            return Ok(new { IdUsuario = ControllerUtils.ObtenerUsuarioId(HttpContext) });
        }

        [HttpPost]
        [HttpPost("EnviarCorreo")]
        public async Task<IActionResult> EnviarCorreo([FromBody] CorreoRequest request)
        {
            if (string.IsNullOrEmpty(request.ToEmail) || string.IsNullOrEmpty(request.Subject) || string.IsNullOrEmpty(request.Body))
            {
                return BadRequest("Todos los campos son obligatorios.");
            }

            try
            {
                await _correoService.EnviarCorreo(request.ToEmail, request.Subject, request.Body);
                return Ok("Correo enviado correctamente.");
            }
            catch (Exception ex)
            {
                // Aquí podrías registrar el error con un logger
                return StatusCode(500, $"Error enviando correo: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("SimulacionImagen")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> SimulacionImagen([FromForm] SimulacionImagen model)
        {
            return Ok(await _imgService.NoUsar_PruebaObtenerExtencionImg(model.Imagen));
        }

        [HttpGet("EnviarNotificacionHub")]
        public async Task<IActionResult> EnviarNotificacionHub()
        {
            var notificacion = new
            {
                mensaje = "Nuevo pedido recibido",
                hora = DateTime.Now
            };

            await _hubContext.Clients.All.SendAsync("RecibirPedido", notificacion);

            return Ok(new { mensaje = "Notificación enviada a SignalR" });
        }

        [HttpPost("EnviarNotificacionPush")]
        public async Task<IActionResult> EnviarNotificacionPush(long IdUsuario)
        {
            _pushNotificationService.EnviarNotificacionAUsuarioAsync(IdUsuario, "Título de prueba", "Este es el cuerpo de la notificación", "/#/seguimientoPedido").Wait();
            return Ok(new { mensaje = "Notificación enviada" });

        }
    }


    public class CorreoRequest
    {
        public string ToEmail { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
    }

    public class SimulacionImagen 
    {
        public string? Nombre { get; set; }
        public string? OtroCampo { get; set; }
        public IFormFile? Imagen { get; set; }
    }
        

}

