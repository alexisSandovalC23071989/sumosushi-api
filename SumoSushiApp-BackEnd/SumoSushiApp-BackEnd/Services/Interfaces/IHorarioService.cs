using SumoSushiApp_BackEnd.DTOs.Horario;
using SumoSushiApp_BackEnd.Models;
using SumoSushiApp_BackEnd.Services.Utils;

namespace SumoSushiApp_BackEnd.Services.Interfaces
{
    public interface IHorarioService
    {
        public Task<RespuestaModel> ObtenerDias();

        public Task<RespuestaModel> ObtenerHorariosRestaurante();

        public Task<RespuestaModel> RegistrarHorario(ActualizarHorarioDTO model);
    }
}
