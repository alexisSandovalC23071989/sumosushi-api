using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SumoSushiApp_BackEnd.Controllers.Utils;
using SumoSushiApp_BackEnd.DTOs.Auth;
using SumoSushiApp_BackEnd.DTOs.Usuario;
using SumoSushiApp_BackEnd.Services.Interfaces;

namespace SumoSushiApp_BackEnd.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        //Inyeccion de dependencias
        private readonly IUsuarioService _usuarioService;
        private readonly IAuthService _authService;
        private readonly ControllerUtils _controllerUtils;

        public AuthController(IUsuarioService usuarioService, IAuthService authService, ControllerUtils controllerUtils)
        {
            _usuarioService = usuarioService;
            _authService = authService;
            _controllerUtils = controllerUtils;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("RegistroUsuario")]
        public async Task<IActionResult> RegistrarUsuario(AuthRegistroDTO model)
        {
            var respuesta = await _usuarioService.RegistrarUsuario(model);
            return Ok(respuesta);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("InicioSesion")]
        public async Task<IActionResult> IniciarSesion(AuthLoginDTO model)
        {
            var (respuesta, refreshToken) = await _usuarioService.InicioSesion(model);

            if (refreshToken != null)
            {
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None,
                    Expires = DateTime.UtcNow.AddDays(30)
                };
                Response.Cookies.Append("refreshToken", refreshToken!, cookieOptions);
            }

            return Ok(respuesta);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("RefreshAccessToken")]
        public async Task<IActionResult> RefreshAccesToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var idUsuario = _controllerUtils.ObtenerUsuarioIdTokenVencido(HttpContext);            

            var (respuesta, nuevoRefreshToken) = await _usuarioService.RefreshAccesToken(idUsuario, refreshToken!);

            if (nuevoRefreshToken != null)
            {
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None,
                    Expires = DateTime.UtcNow.AddDays(30)
                };
                Response.Cookies.Append("refreshToken", nuevoRefreshToken!, cookieOptions);
            }
            return Ok(respuesta);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("SolicitarRecuperarContrasenna")]
        public async Task<IActionResult> SolicitarRecuperarContrasenna(AuthSolicitudRecuperacionDTO model)
        {
            if (string.IsNullOrEmpty(model.Correo))
                return BadRequest();
            var respuesta = await _authService.SolicitarRecuperarContrasenna(model);

            return Ok(respuesta);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("RecuperarContrasenna")]
        public async Task<IActionResult> RecuperarContrasenna(AuthRecuperacionContrasennaDTO model)
        {
            var respuesta = await _authService.RecuperarContrasennaRecuperacion(model);
            return Ok(respuesta);
        }

        [Authorize]
        [HttpPost]
        [Route("CambiarContrasennaLogueado")]
        public async Task<IActionResult> CambiarContrasennaLogueado(SolicitudCambiarContrasennaDTO model)
        {
            var contrasennasIdUsuario = new AuthContrasennasUsuarioDTO(
                model, 
                ControllerUtils.ObtenerUsuarioId(HttpContext)
            );

            var respuesta = await _authService.CambiarContrasennaLogueado(contrasennasIdUsuario);
            return Ok(respuesta);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("CerrarSesion")]
        public async Task<IActionResult> CerrarSesion()
        {
            var refreshToken = new CierreSesionDTO(Request.Cookies["refreshToken"]!);

            var respuesta = await _authService.CierreSesion(refreshToken);

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddDays(-1)
            };
            Response.Cookies.Append("refreshToken", "", cookieOptions);

            return Ok(respuesta);
        }
    }
}
