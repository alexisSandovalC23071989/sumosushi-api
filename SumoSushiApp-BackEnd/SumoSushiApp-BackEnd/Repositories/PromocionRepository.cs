using Dapper;
using SumoSushiApp_BackEnd.Data;
using SumoSushiApp_BackEnd.DTOs.Promocion;
using SumoSushiApp_BackEnd.Repositories.Interfaces;
using SumoSushiApp_BackEnd.Repositories.Utils;
using System.Data;
using System.Reflection;

namespace SumoSushiApp_BackEnd.Repositories
{
    public class PromocionRepository : IPromocionRepository
    {
        private readonly IDBContext _db;
        public PromocionRepository(IDBContext db) => _db = db;

        public async Task<IEnumerable<PromocionDTO>> ObtenerTodasAsync()
        {
            using var c = _db.MySqlConexion();
            return await c.QueryAsync<PromocionDTO>("ObtenerTodasPromociones", commandType: CommandType.StoredProcedure);
            using var conexion = _db.MySqlConexion();
            return await conexion.QueryAsync<PromocionDTO>("ObtenerTodasPromociones", commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<PromocionDTO>> ObtenerActivasAsync()
        {
            using var c = _db.MySqlConexion();
            return await c.QueryAsync<PromocionDTO>("ObtenerPromocionesActivas", commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<PromocionDTO>> ObtenerInactivasAsync()
        {
            using var c = _db.MySqlConexion();
            return await c.QueryAsync<PromocionDTO>("ObtenerPromocionesInactivas", commandType: CommandType.StoredProcedure);
        }

        public async Task<(int Codigo, string Mensaje)> CrearPromocionAsync(PromocionCrearDTO dto)
        {
            using var conexion = _db.MySqlConexion();

            var parametros = RepoUtils.MapearParametros(dto, true);

            await conexion.ExecuteAsync("CrearPromocion", parametros, commandType: CommandType.StoredProcedure);
            return RepoUtils.ObtenerRsultado(parametros);
        }

        public async Task<(int Codigo, string Mensaje)> ActualizarPromocionAsync(PromocionActualizarDTO dto)
        {
            using var conexion = _db.MySqlConexion();
            var parametros = RepoUtils.MapearParametros(dto, true);
            await conexion.ExecuteAsync("ActualizarPromocion", parametros, commandType: CommandType.StoredProcedure);
            return RepoUtils.ObtenerRsultado(parametros);
        }

        public async Task<(int Codigo, string Mensaje)> ActivarPromocionAsync(PromocionCambiarEstadoDTO dto)
        {
            using var conexion = _db.MySqlConexion();
            var parametros = RepoUtils.MapearParametros(dto, post: true);
            await conexion.ExecuteAsync("ActivarPromocion", parametros, commandType: CommandType.StoredProcedure);
            return RepoUtils.ObtenerRsultado(parametros);
        }

        public async Task<(int Codigo, string Mensaje)> DeshabilitarPromocionAsync(PromocionCambiarEstadoDTO dto)
        {
            using var conexion = _db.MySqlConexion();
            var parametros = RepoUtils.MapearParametros(dto, true);
            await conexion.ExecuteAsync("DeshabilitarPromocion", parametros, commandType: CommandType.StoredProcedure);
            return RepoUtils.ObtenerRsultado(parametros);
        }
    }
}