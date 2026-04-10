using SumoSushiApp_BackEnd.Models;

namespace SumoSushiApp_BackEnd.Services.Interfaces
{
    public interface IPresentacionService
    {
        public Task<RespuestaModel> ObtenerPresentaciones();
    }
}
