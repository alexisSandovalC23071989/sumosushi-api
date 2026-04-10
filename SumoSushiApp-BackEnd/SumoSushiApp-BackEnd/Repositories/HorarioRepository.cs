using Dapper;
using SumoSushiApp_BackEnd.Data;
using SumoSushiApp_BackEnd.DTOs.Horario;
using SumoSushiApp_BackEnd.Repositories.Interfaces;
using SumoSushiApp_BackEnd.Repositories.Utils;
using System.Data;

namespace SumoSushiApp_BackEnd.Repositories
{
    public class HorarioRepository : IHorarioRepository
    {
        private readonly IDBContext _dbContext;
        public HorarioRepository(IDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<DiaDTO>> ObtenerDias()
        {
            using var conexion = _dbContext.MySqlConexion();
            return await conexion.QueryAsync<DiaDTO>("ObtenerListaDias", commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<ObtenerHorarioDTO>> ObtenerHorariosPorDia()
        {
            using var conexion = _dbContext.MySqlConexion();
            return await conexion.QueryAsync<ObtenerHorarioDTO>("ObtenerHorariosRestaurante", commandType: CommandType.StoredProcedure);
        }

        public async Task<(int codigoError, string mensaje)> RegistrarHorario(ActualizarHorarioDTO model)
        {
            using var conexion = _dbContext.MySqlConexion();
            var parametros = RepoUtils.MapearParametros(model, true);
            await conexion.ExecuteAsync("RegistrarHorario", parametros, commandType: CommandType.StoredProcedure);
            return RepoUtils.ObtenerRsultado(parametros);
        }
    }
}
