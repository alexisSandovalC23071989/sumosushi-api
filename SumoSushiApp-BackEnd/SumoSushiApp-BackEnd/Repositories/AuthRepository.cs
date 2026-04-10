using Dapper;
using SumoSushiApp_BackEnd.Data;
using SumoSushiApp_BackEnd.DTOs.Auth;
using SumoSushiApp_BackEnd.Repositories.Interfaces;
using SumoSushiApp_BackEnd.Repositories.Utils;
using System.Data;

namespace SumoSushiApp_BackEnd.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IDBContext _dbContext;
        public AuthRepository(IDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<(int codigoError, string mensaje)> CambiarContrasennaRecuperacion(object model)
        {
            using var conexion = _dbContext.MySqlConexion();
            var parametros = RepoUtils.MapearParametros(model, true);
            await conexion.ExecuteAsync("CambiarContrasennaRecuperacion", parametros, commandType: CommandType.StoredProcedure);
            return RepoUtils.ObtenerRsultado(parametros);
        }

        public async Task<ValidacionCambioContrasenna> DatosValidacionCambioContrasenna(AuthContrasennasUsuarioDTO model)
        {
            using var conexion = _dbContext.MySqlConexion();

            var parametros = RepoUtils.MapearParametros(model, false);

            var resultado = await conexion.QueryFirstOrDefaultAsync<ValidacionCambioContrasenna>(
                "DatosValidacionCambioContrasenna", 
                parametros, 
                commandType: CommandType.StoredProcedure);
            return resultado ?? new ValidacionCambioContrasenna();
        }

        public async Task<(int codigoError, string mensaje)> CambiarContrasennaLogueado(object model)
        {
            using var conexion = _dbContext.MySqlConexion();
            var parametros = RepoUtils.MapearParametros(model, true);
            await conexion.ExecuteAsync("CambiarContrasennaLogueado", parametros, commandType: CommandType.StoredProcedure);
            return RepoUtils.ObtenerRsultado(parametros);
        }


    }
}
