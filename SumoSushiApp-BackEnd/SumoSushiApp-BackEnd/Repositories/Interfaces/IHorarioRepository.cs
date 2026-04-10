using SumoSushiApp_BackEnd.DTOs.Horario;
using System.Data;

namespace SumoSushiApp_BackEnd.Repositories.Interfaces
{
    public interface IHorarioRepository
    {
        public Task<IEnumerable<DiaDTO>> ObtenerDias();

        public Task<IEnumerable<ObtenerHorarioDTO>> ObtenerHorariosPorDia();

        public Task<(int codigoError, string mensaje)> RegistrarHorario(ActualizarHorarioDTO model);


    }
}
