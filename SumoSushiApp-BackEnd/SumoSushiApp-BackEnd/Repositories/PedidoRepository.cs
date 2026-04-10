using Dapper;
using SumoSushiApp_BackEnd.Data;
using SumoSushiApp_BackEnd.DTOs.Pedido;
using SumoSushiApp_BackEnd.Repositories.Interfaces;
using SumoSushiApp_BackEnd.Repositories.Utils;
using System.Data;

namespace SumoSushiApp_BackEnd.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly IDBContext _dbContext;
        public PedidoRepository(IDBContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        
        public async Task<(int codigoError, string mensaje)> RegistrarPedido(NuevoPedidoDTO model)
        {
            using var conexion = _dbContext.MySqlConexion();

            var parametros = RepoUtils.MapearParametros(model, true);

            await conexion.ExecuteAsync(
                "RegistrarPedidoCompleto",
                parametros,
                commandType: System.Data.CommandType.StoredProcedure);

            return RepoUtils.ObtenerRsultado(parametros);

        }

        public async Task<IEnumerable<PedidoModel>> ObtenerPedidosAsync()
        {
            var pedidos = new List<PedidoModel>();

            using var connection = _dbContext.CrearConexionMySql();
            await connection.OpenAsync();

            using var multi = await connection.QueryMultipleAsync(
                "ObtenerPedidosConDetalles", 
                commandType: CommandType.StoredProcedure);
                
            var pedidosBase = await multi.ReadAsync<PedidoModel>();
            var promociones = await multi.ReadAsync<PromocionModel>();
            var productos = await multi.ReadAsync<ProductoModel>();

            // Asociar detalles a cada pedido
            foreach (var pedido in pedidosBase)
            {
                pedido.Promociones = promociones.Where(p => p.IdPedido == pedido.IdPedido);
                pedido.Productos = productos.Where(p => p.IdPedido == pedido.IdPedido);
            }

            return pedidosBase;
        }

        public async Task<IEnumerable<PedidoModel>> ObtenerPedidosPorUsuarioAsync(ObtenerPedidosUsuarioDTO model)
        {
            var pedidos = new List<PedidoModel>();

            using var connection = _dbContext.CrearConexionMySql();
            await connection.OpenAsync();
            var parametros = RepoUtils.MapearParametros(model, true);

            using var multi = await connection.QueryMultipleAsync(
                "ObtenerPedidosUsuario",
                parametros,
                commandType: CommandType.StoredProcedure);

            var pedidosBase = await multi.ReadAsync<PedidoModel>();
            var promociones = await multi.ReadAsync<PromocionModel>();
            var productos = await multi.ReadAsync<ProductoModel>();

            foreach (var pedido in pedidosBase)
            {
                pedido.Promociones = promociones.Where(p => p.IdPedido == pedido.IdPedido);
                pedido.Productos = productos.Where(p => p.IdPedido == pedido.IdPedido);
            }
            return pedidosBase;
        }

        public async Task<(int codigoError, string mensaje)> ActualizarEstadoPedido(ActualizarEstadoPedidoDTO model)
        {
            using var conexion = _dbContext.MySqlConexion();
            var parametros = RepoUtils.MapearParametros(model, true);
            await conexion.ExecuteAsync(
                "CambiarEstadoPedido",
                parametros,
                commandType: System.Data.CommandType.StoredProcedure);
            return RepoUtils.ObtenerRsultado(parametros);

        }

        public async Task<InfoActualizarPedidoDTO?> ObtenerInfoActualizarPedido(ActualizarEstadoPedidoDTO model)
        {
            using var conexion = _dbContext.MySqlConexion();
            var parametros = RepoUtils.MapearParametros(model, true);
            return await conexion.QueryFirstOrDefaultAsync<InfoActualizarPedidoDTO>(
                "InfoActualizarPedido",
                parametros,
                commandType: System.Data.CommandType.StoredProcedure);

        }
    }
}
