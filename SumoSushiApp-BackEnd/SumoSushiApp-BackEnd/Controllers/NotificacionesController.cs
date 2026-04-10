using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SumoSushiApp_BackEnd.Controllers.Utils;
using SumoSushiApp_BackEnd.Models;
using SumoSushiApp_BackEnd.Services;
using SumoSushiApp_BackEnd.Services.Interfaces;

namespace SumoSushiApp_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificacionesController : ControllerBase
    {
        private readonly IPushNotificationService _pushService;
        public NotificacionesController(IPushNotificationService pushService)
        {
            _pushService = pushService;
        }

        [Authorize]
        [HttpPost("suscribirse")]
        public IActionResult Suscribirse([FromBody] PushSubscriptionModel model)
        {
            var idUsuario = ControllerUtils.ObtenerUsuarioId(HttpContext);

            _pushService.AgregarSuscripcion(idUsuario, model);

            return Ok();
        }
    }
}

