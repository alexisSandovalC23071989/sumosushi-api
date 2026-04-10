
using Dapper;
using SumoSushiApp_BackEnd.DTOs.Etiqueta;
using SumoSushiApp_BackEnd.DTOs.Menu;
using SumoSushiApp_BackEnd.DTOs.Producto;
using SumoSushiApp_BackEnd.Repositories.Utils;
using System.Data;

namespace SumoSushiApp_BackEnd.Repositories
{
    public interface IProductoRepository
    {
        #region Stheven
        List<ProductoMenuDTO> ObtenerMenuCompleto();
        ProductoMenuDTO? ObtenerDetalleProducto(int id);
        (string mensaje, int codigoError) CalificarProducto(long usuarioId, int productoId, int calificacion);
        #endregion
        public Task<(int codigoError, string mensaje, long IdProducto)> RegistrarProducto(
            NuevoProductoDTO model,
            IDbConnection conexion,
            IDbTransaction transaccion);
        public Task<(int codigoError, string mensaje)> ActualizarProducto(
            ActualizarDatosProductoDTO model,
            IDbConnection conexion,
            IDbTransaction transaccion);

        public Task<(int codigoError, string mensaje)> RegistrarPresentacionProducto(
            RegistroPresentacionProductoDTO model,
            IDbConnection conexion,
            IDbTransaction transaccion);

        public Task<(int codigoError, string mensaje)> ActualizarPresentacionProducto(
            ActualizarPresentacionProductoDTO model,
            IDbConnection conexion,
            IDbTransaction transaccion);

        public Task<(int codigoError, string mensaje)> RegistraEtiquetaProducto(
            EtiquetaProductoDTO model,
            IDbConnection conexion,
            IDbTransaction transaccion);

        
    }     
}

