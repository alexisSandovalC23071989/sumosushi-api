using Dapper;
using SumoSushiApp_BackEnd.Data;
using SumoSushiApp_BackEnd.DTOs.Carrito;
using SumoSushiApp_BackEnd.Repositories.Interfaces;
using SumoSushiApp_BackEnd.Repositories.Utils;
using System.Data;

namespace SumoSushiApp_BackEnd.Repositories
{
    public class CarritoRepository : ICarritoRepository
    {
        private readonly DBContext _context;

        public CarritoRepository(DBContext context)
        {
            _context = context;
        }

        public async Task<(int CodigoError, string Mensaje)> AgregarOActualizarPromocion(CarritoAgregarDTO dto)
        {
            using var conn = _context.MySqlConexion();
            var parametros = RepoUtils.MapearParametros(dto);

            await conn.ExecuteAsync("Carrito_AgregarOActualizarPromocion", parametros, commandType: CommandType.StoredProcedure);
            var codigo = parametros.Get<int>("p_CodigoError");
            var mensaje = parametros.Get<string>("p_Mensaje");
            return (codigo, mensaje);
        }


        public async Task<(int CodigoError, string Mensaje)> AgregarOActualizarPresentacion(CarritoAgregarDTO dto)
        {
            using var conn = _context.MySqlConexion();
            var parametros = new DynamicParameters();
            parametros.Add("p_IdUsuario", dto.IdUsuario);
            parametros.Add("p_IdPresentacionProducto", dto.IdPresentacionProducto);
            parametros.Add("p_Cantidad", dto.Cantidad);
            parametros.Add("p_CodigoError", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("p_Mensaje", dbType: DbType.String, size: 300, direction: ParameterDirection.Output);

            await conn.ExecuteAsync("Carrito_AgregarOActualizarPresentacion", parametros, commandType: CommandType.StoredProcedure);
            var codigo = parametros.Get<int>("p_CodigoError");
            var mensaje = parametros.Get<string>("p_Mensaje");
            return (codigo, mensaje);
        }

        public async Task<(int CodigoError, string Mensaje)> EliminarProducto(CarritoEliminarDTO dto)
        {
            using var conn = _context.MySqlConexion();

            ///// Utlizo cero = null
            if (dto.IdPresentacionProducto == 0)
                dto.IdPresentacionProducto = null;
            if (dto.IdPromocion == 0)
                dto.IdPromocion = null;

            var parametros = new DynamicParameters();
            parametros.Add("p_IdUsuario", dto.IdUsuario);
            parametros.Add("p_IdPresentacionProducto", dto.IdPresentacionProducto);
            parametros.Add("p_IdPromocion", dto.IdPromocion);
            parametros.Add("p_CodigoError", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("p_Mensaje", dbType: DbType.String, size: 300, direction: ParameterDirection.Output);

            await conn.ExecuteAsync("Carrito_Eliminar", parametros, commandType: CommandType.StoredProcedure);

            var codigo = parametros.Get<int>("p_CodigoError");
            var mensaje = parametros.Get<string>("p_Mensaje");
            return (codigo, mensaje);
        }

        public async Task<IEnumerable<CarritoItemDTO>> ObtenerCarrito(long idUsuario)
        {
            using var conn = _context.MySqlConexion();
            return await conn.QueryAsync<CarritoItemDTO>(
                "Carrito_ObtenerPorUsuario", 
                new { p_IdUsuario = idUsuario }, 
                commandType: CommandType.StoredProcedure);
        }
    }
}
