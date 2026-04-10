
using SumoSushiApp_BackEnd.DTOs.Etiqueta;
using System.Data;

namespace SumoSushiApp_BackEnd.Repositories.Interfaces
{
    public interface IEtiquetaRepository
    {
        public Task<IEnumerable<EtiquetasProductosDTO>?> ObtenerEtiquetasProductosTotales();

        public Task<IEnumerable<EtiquetaProductoDTO>> ObtenerEtiquetas();
        public Task<(int codigoError, string mensaje)> EliminarEtiquetasProducto(
            EliminarEtiquetasProductoDTO model,
            IDbConnection conexion,
            IDbTransaction transaccion);
    }
}
