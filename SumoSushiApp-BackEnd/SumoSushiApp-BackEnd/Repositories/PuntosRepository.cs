using Dapper;
using System.Data;
using SumoSushiApp_BackEnd.Data;
using SumoSushiApp_BackEnd.DTOs.Puntos;
using SumoSushiApp_BackEnd.Repositories.Interfaces;

namespace SumoSushiApp_BackEnd.Repositories
{
    public class PuntosRepository : IPuntosRepository
    {
        private readonly IDBContext _dbContext;
        public PuntosRepository(IDBContext dbContext) => _dbContext = dbContext;

        public async Task<PuntosClienteDTO> ObtenerPuntosUsuario(long idUsuario)
        {
            using var conn = _dbContext.MySqlConexion();
            var p = new DynamicParameters();
            p.Add("p_IdUsuario", idUsuario, DbType.UInt64);
            p.Add("p_PuntosAcumulados", dbType: DbType.UInt64, direction: ParameterDirection.Output);
            p.Add("p_PuntosMinimosCanje", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("p_PuntosMaximosCanje", dbType: DbType.Int32, direction: ParameterDirection.Output);

            await conn.ExecuteAsync("ObtenerPuntosUsuario", p, commandType: CommandType.StoredProcedure);

            return new PuntosClienteDTO
            {
                PuntosAcumulados = p.Get<ulong>("p_PuntosAcumulados"),
                PuntosMinimosCanje = p.Get<int>("p_PuntosMinimosCanje"),
                PuntosMaximosCanje = p.Get<int>("p_PuntosMaximosCanje")
            };
        }

        public async Task<(int codigoError, string mensaje)> CanjearPuntosUsuario(long idUsuario, ulong puntosACanjear)
        {
            using var conn = _dbContext.MySqlConexion();
            var p = new DynamicParameters();
            p.Add("p_IdUsuario", idUsuario, DbType.UInt64);
            p.Add("p_PuntosACanjear", puntosACanjear, DbType.UInt64);
            p.Add("p_CodigoError", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("p_Mensaje", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);

            await conn.ExecuteAsync("CanjearPuntosUsuario", p, commandType: CommandType.StoredProcedure);

            return (p.Get<int>("p_CodigoError"), p.Get<string>("p_Mensaje")!);
        }
    }
}