using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SumoSushiApp_BackEnd.Models;

namespace SumoSushiApp_BackEnd.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]

    [Route("api/[controller]")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [Route("CapturarError")]
        public IActionResult CapturarError()
        {
            var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var error = exceptionFeature?.Error;

            var detalleError = new
            {
                Mensaje = error?.Message ?? "Ocurrió un error",
                Fuente = error?.Source,
                Pila = error?.StackTrace
            };

            var respuesta = new RespuestaModel
            {
                Exito = false,
                Mensaje = "Se produjo un error en la aplicación",
                Datos = detalleError // opcional si usas un campo `Datos` para extra info
            };

            return StatusCode(StatusCodes.Status500InternalServerError, respuesta);
        }
    }
}