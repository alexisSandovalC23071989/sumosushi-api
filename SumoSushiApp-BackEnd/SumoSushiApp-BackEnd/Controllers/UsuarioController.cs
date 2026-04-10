using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
// USING PARA UTILIZAR EL METODO ESTATICO PARA OBTENER EL ID DEL USUARIO DESDE EL CONTEXTO HTTP
using SumoSushiApp_BackEnd.Controllers.Utils;
using SumoSushiApp_BackEnd.DTOs.Usuario;
using SumoSushiApp_BackEnd.Services.Interfaces;

namespace SumoSushiApp_BackEnd.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        #region Usuarios
        [Authorize]
        [HttpGet]
        [Route("ObtenerPerfilUsuario")]
        public async Task<IActionResult> ObtenerPerfilUsuario()
        {
            // Obtenemos el ID del usuario desde el contexto HTTP utilizando un método estático
            var idUsuario = ControllerUtils.ObtenerUsuarioId(HttpContext);

            var respuesta = await _usuarioService.ObtenerPerfilUsuario(idUsuario);

            return Ok(respuesta);
        }

        /// Actualiza el perfil del usuario autenticado.
        [HttpPut]
        [Authorize]
        [Route("ActualizarPerfilUsuario")]
        public async Task<IActionResult> ActualizarPerfilUsuario(UsuarioPerfilActualizarDTO model)
        {
            model.IdUsuario = ControllerUtils.ObtenerUsuarioId(HttpContext);
            if (model.IdUsuario <= 0)
                return Unauthorized("ID de usuario no válido o no autenticado.");
            var respuesta = await _usuarioService.ActualizarPerfilUsuario(model);
            return Ok(respuesta);
        }

        /// Desactiva la cuenta del usuario autenticado.
        [HttpPut]
        [Authorize]
        [Route("DesactivarCuenta")]
        public async Task<IActionResult> DesactivarCuenta()
        {
            var idUsuario = ControllerUtils.ObtenerUsuarioId(HttpContext);

            if (idUsuario <= 0)
            {
                return BadRequest("ID de usuario no válido o no autenticado.");
            }

            var respuesta = await _usuarioService.DesactivarCuenta(idUsuario);

            return Ok(respuesta);
        }
        #endregion

        // ===============================================================================================
        //                          ADMIN
        // ===============================================================================================
        #region Admin
        [HttpPut]
        [Authorize(Roles = "Admin")]
        [Route("AdminDesactivarCuenta")]
        public async Task<IActionResult> AdminDesactivarCuenta(AdminGestionCuentasDTO model)
        {
            var respuesta = await _usuarioService.AdminDesactivarCuenta(model);
            return Ok(respuesta);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        [Route("AdminReactivarCuenta")]
        public async Task<IActionResult> AdminReactivarCuenta(AdminGestionCuentasDTO model)
        {
            var respuesta = await _usuarioService.AdminReactivarCuenta(model);
            return Ok(respuesta);
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("ObtenerColaboradoresRestaurante")]
        public async Task<IActionResult> ObtenerColaboradoresRestaurante()
        {
            var respuesta = await _usuarioService.ObtenerColaboradoresRestaurante();
            return Ok(respuesta);
        }

        [HttpPut]
        [Authorize(Roles ="Admin")]
        [Route("ActualizarRolUsuario")]
        public async Task<IActionResult> ActualizarRolUsuario(ActualizarRolDTO model)
        {
            var respuesta = await _usuarioService.ActualizarRolUsuario(model);
            return Ok(respuesta);
        }

        /// <summary>
        /// Utilizamos HttpPost para no enviar el correo de un usuario en la URL, sino en el cuerpo de la solicitud
        /// con la finalidad de mantener la privacidad del correo y poder aplicar filtros de seguridad adicionales
        /// en el DTO con las anotations. 
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("ObtenerUsuarioPorCorreo")]
        public async Task<IActionResult> ObtenerUsuarioPorCorreo([FromBody] ObtenerUsuarioPorCorreoDTO model)
        {
            var respuesta = await _usuarioService.ObtenerUsuarioPorCorreo(model);
            return Ok(respuesta);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("ObtenerUsuariosDeshabilitados")]
        public async Task<IActionResult> ObtenerUsuariosDeshabilitados()
        {
            var respuesta = await _usuarioService.ObtenerUsuariosDeshabilitados();
            return Ok(respuesta);
        }

        #endregion
    }
}