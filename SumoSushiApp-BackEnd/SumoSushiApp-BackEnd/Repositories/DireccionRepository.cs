using Dapper;
using SumoSushiApp_BackEnd.Data;
using SumoSushiApp_BackEnd.DTOs.Direccion;
using SumoSushiApp_BackEnd.Repositories.Interfaces;
using System.Data;

namespace SumoSushiApp_BackEnd.Repositories
{
    public class DireccionRepository : IDireccionRepository
    {
        private readonly IDBContext _dbContext;

        public DireccionRepository(IDBContext context)
        {
            _dbContext = context;
        }

        /// Obtiene todas las direcciones (activas e inactivas) de un usuario.
        public async Task<IEnumerable<DireccionCompletaDTO>> ObtenerDireccionesUsuario(long idUsuario)
        {

            using var conexion = _dbContext.MySqlConexion();

            return await conexion.QueryAsync<DireccionCompletaDTO>(
                "ObtenerDireccionesUsuario",
                new { p_IdUsuario = idUsuario },
                commandType: CommandType.StoredProcedure);
        }

        /// Llama al SP AgregarDireccionUsuario.
        public async Task<(int CodigoError, string Mensaje)> AgregarDireccionUsuario(long idUsuario, DireccionCrearDTO dto)
        {
            using var conexion = _dbContext.MySqlConexion();
            var parametros = new DynamicParameters();
            parametros.Add("p_IdUsuario", idUsuario, DbType.UInt64);
            parametros.Add("p_IdCanton", dto.IdCanton, DbType.Int32);
            parametros.Add("p_IdDistrito", dto.IdDistrito, DbType.Int16);
            parametros.Add("p_Detalle", dto.Detalle, DbType.String);
            parametros.Add("p_CodigoError", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("p_Mensaje", dbType: DbType.String, size: 400, direction: ParameterDirection.Output);

            await conexion.ExecuteAsync(
                "AgregarDireccionUsuario",
                parametros,
                commandType: CommandType.StoredProcedure);

            int codigo = parametros.Get<int>("p_CodigoError");
            string msg = parametros.Get<string>("p_Mensaje")!;
            return (codigo, msg);
        }

        /// Llama al SP ActualizarDireccionUsuario.
        public async Task<(int CodigoError, string Mensaje)> ActualizarDireccionUsuario(long idUsuario, DireccionActualizarDTO dto)
        {
            using var conexion = _dbContext.MySqlConexion();
            var parametros = new DynamicParameters();
            parametros.Add("p_IdDireccionAnterior", dto.IdDireccionAnterior, DbType.UInt64);
            parametros.Add("p_IdUsuario", idUsuario, DbType.UInt64);
            parametros.Add("p_IdDistritoNuevo", dto.IdDistritoNuevo, DbType.Int16);
            parametros.Add("p_DetalleNuevo", dto.DetalleNuevo, DbType.String);
            parametros.Add("p_CodigoError", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("p_Mensaje", dbType: DbType.String, size: 400, direction: ParameterDirection.Output);

            await conexion.ExecuteAsync(
                "ActualizarDireccionUsuario",
                parametros,
                commandType: CommandType.StoredProcedure);

            int codigo = parametros.Get<int>("p_CodigoError");
            string msg = parametros.Get<string>("p_Mensaje")!;
            return (codigo, msg);
        }

        /// Llama al SP DeshabilitarDireccionUsuario.
        public async Task<(int CodigoError, string Mensaje)> DeshabilitarDireccionUsuario(long idUsuario, long idDireccion)
        {
            using var conexion = _dbContext.MySqlConexion();
            var parametros = new DynamicParameters();
            parametros.Add("p_IdDireccion", idDireccion, DbType.UInt64);
            parametros.Add("p_IdUsuario", idUsuario, DbType.UInt64);
            parametros.Add("p_CodigoError", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("p_Mensaje", dbType: DbType.String, size: 400, direction: ParameterDirection.Output);

            await conexion.ExecuteAsync(
                "DeshabilitarDireccionUsuario",
                parametros,
                commandType: CommandType.StoredProcedure);

            int codigo = parametros.Get<int>("p_CodigoError");
            string msg = parametros.Get<string>("p_Mensaje")!;
            return (codigo, msg);
        }

        public async Task<IEnumerable<CantonDTO>> ObtenerCantones()
        {
            using var cn = _dbContext.MySqlConexion();
            return await cn.QueryAsync<CantonDTO>(
                "ObtenerCantones",
                commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<DistritoDTO>> ObtenerDistritosPorCanton()
        {
            using var cn = _dbContext.MySqlConexion();
            return await cn.QueryAsync<DistritoDTO>(
                "ObtenerDistritosPorCanton",
                commandType: CommandType.StoredProcedure);
        }
    }
}