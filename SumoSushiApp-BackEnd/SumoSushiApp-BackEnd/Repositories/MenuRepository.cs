using Dapper;
using SumoSushiApp_BackEnd.Data;
using SumoSushiApp_BackEnd.DTOs.Menu;
using SumoSushiApp_BackEnd.DTOs.Producto;
using SumoSushiApp_BackEnd.Repositories.Interfaces;
using System.Data;

namespace SumoSushiApp_BackEnd.Repositories
{
    public class MenuRepository : IMenuRepository
    {
        private readonly IDBContext _dbContext;

        public MenuRepository(IDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<VersionMenuDTO?> ObtenerVersion()
        {
            using var conexion = _dbContext.MySqlConexion();
            return await conexion.QueryFirstOrDefaultAsync<VersionMenuDTO>(
                "ObtenerVersionMenu",
                commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<CategoriasMenuDTO>> ObtenerCategorias()
        {
            using var conexion = _dbContext.MySqlConexion();
            return await conexion.QueryAsync<CategoriasMenuDTO>(
                "ObtenerCategoriasActivas",
                commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<PromocionesMenuDTO>> ObtenerPromociones()
        {
            using var conexion = _dbContext.MySqlConexion();
            return await conexion.QueryAsync<PromocionesMenuDTO>(
                "ObtenerPromocionesActivas",
                commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<ProductosMenuDTO>> ObtenerProductosActivos()
        {
            using var conexion = _dbContext.MySqlConexion();
            return await conexion.QueryAsync<ProductosMenuDTO>(
                "ObtenerProductosActivos",
                commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<PresentacionMenuDTO>> ObtenerPresentacionesActivas()
        {
            using var conexion = _dbContext.MySqlConexion();
            return await conexion.QueryAsync<PresentacionMenuDTO>(
                "ObtenerPresentacionesActivas",
                commandType: CommandType.StoredProcedure);
        }

        // ADMINISTRATIVO
        public async Task<IEnumerable<PresentacionProductoDTO>> ObtenerPresentacionesTotales()
        {
            using var conexion = _dbContext.MySqlConexion();
            return await conexion.QueryAsync<PresentacionProductoDTO>(
                "ObtenerPresentacionesTotales",
                commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<ProductosTotalesDTO>> ObtenerProductosTotales()
        {
            using var conexion = _dbContext.MySqlConexion();
            return await conexion.QueryAsync<ProductosTotalesDTO>(
                "ObtenerProductosTotales",
                commandType: CommandType.StoredProcedure);
        }

        public async Task<(int codigoError, string mensaje)> ActualizarVersionMenu()
        {
            using var conexion = _dbContext.MySqlConexion();

            var parametros = new DynamicParameters();
            parametros.Add("p_CodigoError", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("p_Mensaje", dbType: DbType.String, size: 1000, direction: ParameterDirection.Output);

            await conexion.ExecuteAsync("ActualizarVersionMenu", parametros, commandType: CommandType.StoredProcedure);

            int codigoError = parametros.Get<int>("p_CodigoError");
            string mensaje = parametros.Get<string>("p_Mensaje");

            return (codigoError, mensaje);
        }
    }
}
