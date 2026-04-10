using Dapper;
using SumoSushiApp_BackEnd.Data;
using SumoSushiApp_BackEnd.DTOs.Usuario;
using SumoSushiApp_BackEnd.Repositories.Interfaces;
using SumoSushiApp_BackEnd.Repositories.Utils;
using SumoSushiApp_BackEnd.Services.Utils;
using System.Data;

namespace SumoSushiApp_BackEnd.Repositories
{
    public class TelefonoRepository : ITelefonoRepository
    {
        private readonly IDBContext _dbContext;

        public TelefonoRepository(IDBContext context)
        {
            _dbContext = context;
        }

        public async Task<IEnumerable<string?>> ObtenerTelefonosUsuario(long idUsuario)
        {
            using var conexion = _dbContext.MySqlConexion();
            return await conexion.QueryAsync<string?>(
                "ObtenerTelefonosUsuario",
                new { p_IdUsuario = idUsuario },
                commandType: CommandType.StoredProcedure);
        }
        public async Task<(int codigoError, string mensaje)> RegistrarTelefono(NuevoTelefonoUsuarioDTO model)
        {
            using var conexion = _dbContext.MySqlConexion();
            var parametros = RepoUtils.MapearParametros(model, true);
            await conexion.ExecuteAsync("AgregarTelefonoUsuario", parametros, commandType: CommandType.StoredProcedure);
            return RepoUtils.ObtenerRsultado(parametros);
        }

        public async Task<(int codigoError, string mensaje)> EliminarTelefono(EliminarTelefonoUsuarioDTO model)
        {
            using var conexion = _dbContext.MySqlConexion();
            var parametros = RepoUtils.MapearParametros(model, true);
            await conexion.ExecuteAsync("EliminarTelefonoUsuario", parametros, commandType: CommandType.StoredProcedure);
            return RepoUtils.ObtenerRsultado(parametros);
        }

    }
}