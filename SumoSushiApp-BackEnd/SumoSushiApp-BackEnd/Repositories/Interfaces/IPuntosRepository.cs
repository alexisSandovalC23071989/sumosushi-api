using SumoSushiApp_BackEnd.DTOs.Puntos;

namespace SumoSushiApp_BackEnd.Repositories.Interfaces
{
    public interface IPuntosRepository
    {
        Task<PuntosClienteDTO> ObtenerPuntosUsuario(long idUsuario);
        Task<(int codigoError, string mensaje)> CanjearPuntosUsuario(long idUsuario, ulong puntosACanjear);
    }
}