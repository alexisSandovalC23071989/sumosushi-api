using SumoSushiApp_BackEnd.Models;
using SumoSushiApp_BackEnd.Repositories.Interfaces;
using SumoSushiApp_BackEnd.Services.Interfaces;
using SumoSushiApp_BackEnd.Services.Utils;

namespace SumoSushiApp_BackEnd.Services
{
    public class PuntosService : IPuntosService
    {
        private readonly IPuntosRepository _repo;
        public PuntosService(IPuntosRepository repo) => _repo = repo;

        public async Task<RespuestaModel> ObtenerPuntosUsuario(long idUsuario)
        {
            var puntos = await _repo.ObtenerPuntosUsuario(idUsuario);
            return RespuestasUtils.GenerarRespuesta(puntos);
        }

        public async Task<RespuestaModel> CanjearPuntosUsuario(long idUsuario, ulong puntosACanjear)
        {
            var (code, msg) = await _repo.CanjearPuntosUsuario(idUsuario, puntosACanjear);
            return RespuestasUtils.GenerarRespuesta(code, msg);
        }
    }
}