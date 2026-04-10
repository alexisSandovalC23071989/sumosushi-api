using SumoSushiApp_BackEnd.DTOs.Categoria;
using SumoSushiApp_BackEnd.DTOs.Etiqueta;
using SumoSushiApp_BackEnd.DTOs.Menu;
using SumoSushiApp_BackEnd.DTOs.Producto;
using SumoSushiApp_BackEnd.Models;
using SumoSushiApp_BackEnd.Repositories.Interfaces;
using SumoSushiApp_BackEnd.Services.Interfaces;
using SumoSushiApp_BackEnd.Services.Utils;

namespace SumoSushiApp_BackEnd.Services
{
    public class MenuService : IMenuService
    {
        private readonly IMenuRepository _menuRepository;
        private readonly IEtiquetaRepository _etiquetaRepository;
        public MenuService(IMenuRepository menuRepository, IEtiquetaRepository etiquetaRepository)
        {
            _menuRepository = menuRepository;
            _etiquetaRepository = etiquetaRepository;
        }
        public async Task<RespuestaModel> ObtenerVersion()
        {
            var version = await _menuRepository.ObtenerVersion();
            return RespuestasUtils.GenerarRespuesta(version!);
        }

        public async Task<RespuestaModel> ObtenerProductosPresentacionesTotales()
        {
            var productosTask = _menuRepository.ObtenerProductosTotales();
            var presentacionesTask = _menuRepository.ObtenerPresentacionesTotales();
            var etiquetasTask = _etiquetaRepository.ObtenerEtiquetasProductosTotales();

            await Task.WhenAll(productosTask, presentacionesTask, etiquetasTask);

            var productos = productosTask.Result;
            var presentaciones = presentacionesTask.Result ?? new List<PresentacionProductoDTO>();
            var etiquetas = etiquetasTask.Result ?? new List<EtiquetasProductosDTO>();

            if (productos is null)
                return RespuestasUtils.GenerarRespuesta(1, "No se encontraron productos");

            var presentacionesPorProducto = presentaciones.ToLookup(p => p.IdProducto);
            var etiquetasPorProducto = etiquetas.ToLookup(e => e.IdProducto);

            foreach (var producto in productos)
            {
                producto.Presentaciones = presentacionesPorProducto[producto.IdProducto].ToList();
                producto.Etiquetas = etiquetasPorProducto[producto.IdProducto].ToList();
            }

            return RespuestasUtils.GenerarRespuesta(productos);
        }

        public async Task<RespuestaModel> ObtenerMenuCompleto()
        {
            var versionTask = _menuRepository.ObtenerVersion();
            var promocionesTask = _menuRepository.ObtenerPromociones();
            var categoriasTask = _menuRepository.ObtenerCategorias();
            var productosTask = _menuRepository.ObtenerProductosActivos();
            var presentacionesTask = _menuRepository.ObtenerPresentacionesActivas();

            await Task.WhenAll(versionTask, promocionesTask, categoriasTask, productosTask, presentacionesTask);

            var version = await versionTask;
            var promociones = await promocionesTask;
            var categorias = await categoriasTask;
            var productos = await productosTask;
            var presentaciones = await presentacionesTask;

            if (promociones == null || categorias == null || productos == null || presentaciones == null)
                return RespuestasUtils.GenerarRespuesta(1, "No se pudo obtener el menú completo");

            var menu = ArmarMenu(
                version!.VersionMenu, 
                promociones,
                categorias,
                productos,
                presentaciones
            );

            return RespuestasUtils.GenerarRespuesta(menu);
        }

        public MenuCompletoDTO ArmarMenu(
        long version,
        IEnumerable<PromocionesMenuDTO> promociones,
        IEnumerable<CategoriasMenuDTO> categorias,
        IEnumerable<ProductosMenuDTO> productos,
        IEnumerable<PresentacionMenuDTO> presentaciones)
        {
            var presentacionesPorProducto = presentaciones
                .GroupBy(p => p.IdProducto)
                .ToDictionary(g => g.Key, g => g.ToList());

            var categoriasConProductos = categorias.Select(cat =>
            {
                var productosDeCategoria = productos
                    .Where(prod => prod.IdCategoria == cat.IdCategoria)
                    .Select(prod =>
                    {
                        presentacionesPorProducto.TryGetValue(prod.IdProducto, out var presentacionesDelProducto);

                        return new ProductosMenuDTO
                        {
                            IdProducto = prod.IdProducto,
                            Nombre = prod.Nombre,
                            Descripcion = prod.Descripcion,
                            ImagenUrl = prod.ImagenUrl,
                            CanjeablePuntos = prod.CanjeablePuntos,
                            IdCategoria = prod.IdCategoria,
                            Categoria = prod.Categoria,
                            Estado = prod.Estado,
                            Etiquetas = prod.Etiquetas,
                            Presentaciones = presentacionesDelProducto ?? new List<PresentacionMenuDTO>()
                        };
                    }).ToList();

                return new CategoriasMenuDTO
                {
                    IdCategoria = cat.IdCategoria,
                    Categoria = cat.Categoria,
                    Productos = productosDeCategoria
                };
            })

            .Where(cat => cat.Productos != null && cat.Productos.Any())
            .ToArray();

            return new MenuCompletoDTO
            {
                VersionMenu = version,
                Promociones = promociones,
                Categorias = categoriasConProductos
            };
        }

    }
}