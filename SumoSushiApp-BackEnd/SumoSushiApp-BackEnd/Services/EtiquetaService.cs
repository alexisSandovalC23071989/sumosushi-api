using SumoSushiApp_BackEnd.Models;
using SumoSushiApp_BackEnd.Repositories.Interfaces;
using SumoSushiApp_BackEnd.Services.Interfaces;
using SumoSushiApp_BackEnd.Services.Utils;

namespace SumoSushiApp_BackEnd.Services
{
    public class EtiquetaService : IEtiquetaService
    {
        private readonly IEtiquetaRepository _etiquetaRepository;
        public EtiquetaService(IEtiquetaRepository etiquetaRepository)
        {
            _etiquetaRepository = etiquetaRepository;
        }        

        public async Task<RespuestaModel> ObtenerEtiquetas()
        {
            var etiquetas = await _etiquetaRepository.ObtenerEtiquetas();
            return RespuestasUtils.GenerarRespuesta(etiquetas);

        }
    }
}
