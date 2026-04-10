using Dapper;
using SumoSushiApp_BackEnd.Data;
using SumoSushiApp_BackEnd.DTOs.Etiqueta;
using SumoSushiApp_BackEnd.Repositories.Interfaces;
using SumoSushiApp_BackEnd.Repositories.Utils;
using System.Data;

namespace SumoSushiApp_BackEnd.Repositories
{
    public class EtiquetaRepository: IEtiquetaRepository
    {
        private readonly IDBContext _context;
        public EtiquetaRepository(IDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EtiquetasProductosDTO>?> ObtenerEtiquetasProductosTotales()
        {
            using var conexion = _context.MySqlConexion();
            return await conexion.QueryAsync<EtiquetasProductosDTO>(
                "ObtenerEtiquetasProductosTotales",
                commandType: CommandType.StoredProcedure);

        }

        public async Task<IEnumerable<EtiquetaProductoDTO>> ObtenerEtiquetas()
        {
            using var conexion = _context.MySqlConexion();
            return await conexion.QueryAsync<EtiquetaProductoDTO>(
                "ObtenerEtiquetas",
                commandType: CommandType.StoredProcedure);
        }

        public async Task<(int codigoError, string mensaje)> EliminarEtiquetasProducto(
            EliminarEtiquetasProductoDTO model,
            IDbConnection conexion,
            IDbTransaction transaccion
            )
        {
            var parametros = RepoUtils.MapearParametros(model, true);
            await conexion.ExecuteAsync(
                "EliminarEtiquetasProducto",
                parametros,
                transaction: transaccion,
                commandType: CommandType.StoredProcedure);
            return RepoUtils.ObtenerRsultado(parametros);
        }
       
    }
}
