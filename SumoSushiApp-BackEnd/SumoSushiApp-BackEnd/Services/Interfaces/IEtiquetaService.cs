using SumoSushiApp_BackEnd.Models;

namespace SumoSushiApp_BackEnd.Services.Interfaces
{
    public interface IEtiquetaService
    {
        public Task<RespuestaModel> ObtenerEtiquetas();
    }
}
