using SumoSushiApp_BackEnd.DTOs.Menu;
using SumoSushiApp_BackEnd.DTOs.Producto;
using System.Data;

namespace SumoSushiApp_BackEnd.Repositories.Interfaces
{
    public interface IMenuRepository
    {
        public Task<VersionMenuDTO?> ObtenerVersion();
        public Task<IEnumerable<CategoriasMenuDTO>> ObtenerCategorias();
        public Task<IEnumerable<PromocionesMenuDTO>> ObtenerPromociones();
        public Task<IEnumerable<ProductosMenuDTO>> ObtenerProductosActivos();
        public Task<IEnumerable<PresentacionMenuDTO>> ObtenerPresentacionesActivas();
        public Task<IEnumerable<PresentacionProductoDTO>> ObtenerPresentacionesTotales();
        public Task<IEnumerable<ProductosTotalesDTO>> ObtenerProductosTotales();
        public Task<(int codigoError, string mensaje)> ActualizarVersionMenu();

    }
}
