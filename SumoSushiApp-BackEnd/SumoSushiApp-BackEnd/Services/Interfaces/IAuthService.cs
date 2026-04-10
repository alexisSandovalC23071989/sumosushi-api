using SumoSushiApp_BackEnd.DTOs.Auth;
using SumoSushiApp_BackEnd.Models;

namespace SumoSushiApp_BackEnd.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<RespuestaModel> SolicitarRecuperarContrasenna(AuthSolicitudRecuperacionDTO model);
        public Task<RespuestaModel> RecuperarContrasennaRecuperacion(AuthRecuperacionContrasennaDTO model);
        public Task<RespuestaModel> CambiarContrasennaLogueado(AuthContrasennasUsuarioDTO model);
        public Task<RespuestaModel> CierreSesion(CierreSesionDTO model);
    }
}
