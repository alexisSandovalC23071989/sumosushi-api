using Dapper;
using SumoSushiApp_BackEnd.Data;
using SumoSushiApp_BackEnd.DTOs.Auth;
using SumoSushiApp_BackEnd.DTOs.Token;
using SumoSushiApp_BackEnd.Repositories.Interfaces;
using SumoSushiApp_BackEnd.Repositories.Utils;
using System.Data;

namespace SumoSushiApp_BackEnd.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IDBContext _dbContext;
        public TokenRepository(IDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<(int, string)> RegistrarRefreshToken(AuthNuevoRefreshTokenDTO model)
        {
            using var conexion = _dbContext.MySqlConexion();
            var parametros = RepoUtils.MapearParametros(model, true);
            await conexion.ExecuteAsync("RegistrarRefreshToken", parametros, commandType: System.Data.CommandType.StoredProcedure);
            return RepoUtils.ObtenerRsultado(parametros);
        }

        public async Task<AuthRefreshTokenInfoDTO?> UtilizarRefreshToken(string refreshToken)
        {
            using var conexion = _dbContext.MySqlConexion();

                return await conexion.QueryFirstOrDefaultAsync<AuthRefreshTokenInfoDTO>(
                    "UtilizarRefreshToken",
                    new { p_RefreshToken = refreshToken },
                    commandType: CommandType.StoredProcedure);
        }


        public async Task<(int, string)> RegistrarTokenRecuperacion(AuthTokenRecuperacionDTO model)
        {
            using var conexion = _dbContext.MySqlConexion();
            var parametros = RepoUtils.MapearParametros(model, true);
            await conexion.ExecuteAsync("RegistrarTokenRecuperacion", parametros, commandType: CommandType.StoredProcedure);
            return RepoUtils.ObtenerRsultado(parametros);
        }

        public async Task<AuthTokenRecuperacionDTO?> UtilizarTokenRecuperacion(AuthRecuperacionContrasennaDTO model)
        {
            using var conexion = _dbContext.MySqlConexion();
            var parametros = RepoUtils.MapearParametros(model, false);

            return await conexion.QueryFirstOrDefaultAsync<AuthTokenRecuperacionDTO>(
                "UtilizarTokenRecuperacion",
                RepoUtils.MapearParametros(model, false),
                commandType: CommandType.StoredProcedure);
        }

        public async Task<(int, string)> RevocarRefreshToken(CierreSesionDTO model)
        {
            using var conexion = _dbContext.MySqlConexion();
            var parametros = RepoUtils.MapearParametros(model, true);
            await conexion.ExecuteAsync("RevocarRefreshToken", parametros, commandType: CommandType.StoredProcedure);
            return RepoUtils.ObtenerRsultado(parametros);
        }
        
    }
}
