using SumoSushiApp_BackEnd.DTOs.Auth;
using SumoSushiApp_BackEnd.DTOs.Usuario;
using SumoSushiApp_BackEnd.Repositories.Utils;
using System.Data;

namespace SumoSushiApp_BackEnd.Repositories.Interfaces
{
    public interface IUsuarioRepository
    {
        public Task<(int CodigoError, string Mensaje)> RegistrarUsuario(AuthRegistroDTO model);
        public Task<AuthValidacionDTO?> ObtenerDatosInicioSesion(string correo);
        public Task<UsuarioPerfilCompletoDTO?> ObtenerPerfilUsuario(long idUsuario);
        public Task<(int CodigoError, string Mensaje)> ActualizarPerfilUsuario(UsuarioPerfilActualizarDTO model);
        public Task<(int CodigoError, string Mensaje)> DesactivarCuentaUsuario(long idUsuario);
        public Task<string?> ObtenerRolUsuario(long idUsuario);
        public Task<InfoUsuarioDTO?> ObtenerUsuarioPorCorreo(ObtenerUsuarioPorCorreoDTO model);
        public Task<(int codigoError, string mensaje)> ActualizarRolUsuario(ActualizarRolDTO model);
        public Task<(int codigoError, string mensaje)> AdminDesactivarCuenta(AdminGestionCuentasDTO model);
        public Task<(int codigoError, string mensaje)> AdminReactivarCuenta(AdminGestionCuentasDTO model);
        public Task<IEnumerable<InfoUsuarioDTO>> ObtenerColaboradoresRestaurante();
        public Task<IEnumerable<InfoUsuarioDTO>> ObtenerUsuariosDeshabilitados();        
    }
}