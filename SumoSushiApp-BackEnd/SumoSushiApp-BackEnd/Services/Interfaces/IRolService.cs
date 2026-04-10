using SumoSushiApp_BackEnd.Models;

namespace SumoSushiApp_BackEnd.Services.Interfaces
{
    public interface IRolService
    {
        public Task<RespuestaModel> ObtenerRoles();
    }
}
