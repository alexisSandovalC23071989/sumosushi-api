using SumoSushiApp_BackEnd.Models;

namespace SumoSushiApp_BackEnd.Services.Interfaces
{
    public interface IPuntosService
    {
        Task<RespuestaModel> ObtenerPuntosUsuario(long idUsuario);
        Task<RespuestaModel> CanjearPuntosUsuario(long idUsuario, ulong puntosACanjear);
    }
}