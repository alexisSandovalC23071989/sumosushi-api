using SumoSushiApp_BackEnd.DTOs.Restaurante;
using SumoSushiApp_BackEnd.Models;
using SumoSushiApp_BackEnd.Repositories.Interfaces;
using SumoSushiApp_BackEnd.Services.Interfaces;
using SumoSushiApp_BackEnd.Services.Utils;

namespace SumoSushiApp_BackEnd.Services
{
    public class RestauranteService : IRestauranteService
    {
        private readonly IRestauranteRepository _restauranteRepository;

        public RestauranteService(IRestauranteRepository restauranteRepository)
        {
            _restauranteRepository = restauranteRepository;
        }

        public async Task<RespuestaModel> ObtenerInfoRestaurante()
        {
            var infoRestaurante = await _restauranteRepository.ObtenerInfoRestaurante();
            return RespuestasUtils.GenerarRespuesta(infoRestaurante!);
        }

        public async Task<RespuestaModel> ActualizarInfoRestaurante(InfoRestauranteDTO model)
        {
            var (codigoError, mensaje) = await _restauranteRepository.ActualizarInfoRestaurante(model);
            return RespuestasUtils.GenerarRespuesta(codigoError, mensaje);
        }
    }
}
