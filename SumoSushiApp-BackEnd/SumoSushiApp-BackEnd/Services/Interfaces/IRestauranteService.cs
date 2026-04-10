

using SumoSushiApp_BackEnd.DTOs.Restaurante;
using SumoSushiApp_BackEnd.Models;
using SumoSushiApp_BackEnd.Services.Utils;

namespace SumoSushiApp_BackEnd.Services.Interfaces
{
    public interface IRestauranteService
    {
        public Task<RespuestaModel> ObtenerInfoRestaurante();
        public Task<RespuestaModel> ActualizarInfoRestaurante(InfoRestauranteDTO model);

    }
}
