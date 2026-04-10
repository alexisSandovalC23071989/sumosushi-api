using SumoSushiApp_BackEnd.Models;
using SumoSushiApp_BackEnd.Repositories.Interfaces;
using SumoSushiApp_BackEnd.Services.Interfaces;
using SumoSushiApp_BackEnd.Services.Utils;

namespace SumoSushiApp_BackEnd.Services
{
    public class PresentacionService : IPresentacionService
    {
        private readonly IPresentacionRepository _presentacionRepository;
        public PresentacionService(IPresentacionRepository presentacionRepository)
        {
            _presentacionRepository = presentacionRepository;
        }

        public async Task<RespuestaModel> ObtenerPresentaciones()
        {
            var etiquetas = await _presentacionRepository.ObtenerEtiquetas();
            return RespuestasUtils.GenerarRespuesta(etiquetas);
        }
    }
}
