using SumoSushiApp_BackEnd.DTOs.Auth;
using SumoSushiApp_BackEnd.DTOs.Usuario;
using SumoSushiApp_BackEnd.Models;
using SumoSushiApp_BackEnd.Services.Utils;

namespace SumoSushiApp_BackEnd.Services.Interfaces
{
    public interface IUsuarioService
    {
        public Task<RespuestaModel> RegistrarUsuario(AuthRegistroDTO model);
        public Task<(RespuestaModel, string?)> InicioSesion(AuthLoginDTO model);
        public Task<(RespuestaModel?, string?)> RefreshAccesToken(long idUsuario, string refreshToken);
        public Task<RespuestaModel> ObtenerPerfilUsuario(long idUsuario);
        public Task<RespuestaModel> ActualizarPerfilUsuario(UsuarioPerfilActualizarDTO model);
        public Task<RespuestaModel> DesactivarCuenta(long idUsuario);
        public Task<RespuestaModel> ActualizarRolUsuario(ActualizarRolDTO model);
        public Task<RespuestaModel> AdminDesactivarCuenta(AdminGestionCuentasDTO model);
        public Task<RespuestaModel> AdminReactivarCuenta(AdminGestionCuentasDTO model);
        public Task<RespuestaModel> ObtenerColaboradoresRestaurante();
        public Task<RespuestaModel> ObtenerUsuarioPorCorreo(ObtenerUsuarioPorCorreoDTO model);
        public Task<RespuestaModel> ObtenerUsuariosDeshabilitados();
    }
}