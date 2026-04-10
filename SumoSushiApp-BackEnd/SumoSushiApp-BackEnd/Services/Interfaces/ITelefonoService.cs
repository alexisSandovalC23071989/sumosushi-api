using SumoSushiApp_BackEnd.DTOs.Usuario;
using SumoSushiApp_BackEnd.Models;
using SumoSushiApp_BackEnd.Services.Utils;

namespace SumoSushiApp_BackEnd.Services.Interfaces
{
    public interface ITelefonoService
    {
        public Task<RespuestaModel> RegistrarTelefono(NuevoTelefonoUsuarioDTO model);
        public Task<RespuestaModel> EliminarTelefono(EliminarTelefonoUsuarioDTO model);
    }
}
