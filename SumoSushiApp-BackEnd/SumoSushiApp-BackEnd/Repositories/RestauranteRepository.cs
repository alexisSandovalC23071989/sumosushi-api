using Dapper;
using SumoSushiApp_BackEnd.Data;
using SumoSushiApp_BackEnd.DTOs.Restaurante;
using SumoSushiApp_BackEnd.Repositories.Interfaces;
using SumoSushiApp_BackEnd.Repositories.Utils;

namespace SumoSushiApp_BackEnd.Repositories
{
    public class RestauranteRepository : IRestauranteRepository
    {
        private readonly IDBContext _dbContext;
        public RestauranteRepository(IDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<InfoRestauranteDTO?> ObtenerInfoRestaurante()
        {
            using var conexion = _dbContext.MySqlConexion();
            var infoRestaurante = await conexion.QueryFirstOrDefaultAsync<InfoRestauranteDTO>(
                "ObtenerInfoRestaurante", 
                commandType: System.Data.CommandType.StoredProcedure);
            
            return infoRestaurante;
        }

        public async Task<(int codigoError, string mensaje)> ActualizarInfoRestaurante(InfoRestauranteDTO model)
        {
            using var conexion = _dbContext.MySqlConexion();
            var parametros = RepoUtils.MapearParametros(model, true);
            await conexion.ExecuteAsync("ActualizarInfoRestaurante", 
                parametros, 
                commandType: System.Data.CommandType.StoredProcedure);            
            return RepoUtils.ObtenerRsultado(parametros);
        }
    }
}
