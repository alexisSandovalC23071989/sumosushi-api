using SumoSushiApp_BackEnd.DTOs.Auth;

namespace SumoSushiApp_BackEnd.Repositories.Interfaces
{
    public interface IAuthRepository
    {
        public Task<(int codigoError, string mensaje)> CambiarContrasennaRecuperacion(object model);
        public Task<ValidacionCambioContrasenna> DatosValidacionCambioContrasenna(AuthContrasennasUsuarioDTO model);
        public Task<(int codigoError, string mensaje)> CambiarContrasennaLogueado(object model);
    }
}
