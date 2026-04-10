using SumoSushiApp_BackEnd.DTOs.Horario;
using SumoSushiApp_BackEnd.Models;
using SumoSushiApp_BackEnd.Repositories.Interfaces;
using SumoSushiApp_BackEnd.Services.Interfaces;
using SumoSushiApp_BackEnd.Services.Utils;

namespace SumoSushiApp_BackEnd.Services
{
    public class HorarioService : IHorarioService
    {
        private readonly IHorarioRepository _horarioRepository;
        public HorarioService(IHorarioRepository horarioRepository)
        {
            _horarioRepository = horarioRepository;
        }
        
        public async Task<RespuestaModel> ObtenerDias()
        {
            var respuesta = await _horarioRepository.ObtenerDias();
            return RespuestasUtils.GenerarRespuesta(respuesta);
        }

        public async Task<RespuestaModel> ObtenerHorariosRestaurante()
        {
            var respuesta = await _horarioRepository.ObtenerHorariosPorDia();
            return RespuestasUtils.GenerarRespuesta(respuesta);
        }

        public async Task<RespuestaModel> RegistrarHorario(ActualizarHorarioDTO model)
        {
            var (codigoError, mensaje) = await _horarioRepository.RegistrarHorario(model);
            return RespuestasUtils.GenerarRespuesta(codigoError, mensaje);
        }
    }
}
