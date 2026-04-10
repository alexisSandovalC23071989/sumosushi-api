using SumoSushiApp_BackEnd.Models;

namespace SumoSushiApp_BackEnd.Services.Interfaces
{
    public interface IMenuService
    {
        public Task<RespuestaModel> ObtenerVersion();

        public Task<RespuestaModel> ObtenerProductosPresentacionesTotales();

        public Task<RespuestaModel> ObtenerMenuCompleto();
    }
}
