using SumoSushiApp_BackEnd.DTOs.Restaurante;

namespace SumoSushiApp_BackEnd.Repositories.Interfaces
{
    public interface IRestauranteRepository
    {
        public Task<InfoRestauranteDTO?> ObtenerInfoRestaurante();
        public Task<(int codigoError, string mensaje)> ActualizarInfoRestaurante(InfoRestauranteDTO model);
    }
}
