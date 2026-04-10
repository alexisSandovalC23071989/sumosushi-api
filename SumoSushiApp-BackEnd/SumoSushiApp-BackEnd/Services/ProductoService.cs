using SumoSushiApp_BackEnd.Data;
using SumoSushiApp_BackEnd.DTOs.Etiqueta;
using SumoSushiApp_BackEnd.DTOs.Producto;
using SumoSushiApp_BackEnd.Models;
using SumoSushiApp_BackEnd.Repositories;
using SumoSushiApp_BackEnd.Repositories.Interfaces;
using SumoSushiApp_BackEnd.Services.Interfaces;
using SumoSushiApp_BackEnd.Services.Utils;
using System.Data;
using System.Security.Cryptography.X509Certificates;

namespace SumoSushiApp_BackEnd.Services
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _productoRepository;
        private readonly IMenuRepository _menuRepository;
        private readonly IDBContext _dbContext;
        private readonly IImgService _imgService;
        private readonly IEtiquetaRepository _etiquetaRepository;

        public ProductoService(
            IProductoRepository productoRepository, 
            IDBContext dbContext,
            IImgService imgService,
            IMenuRepository menuRepository,
            IEtiquetaRepository etiquetaRepository
            ) 
        {
            _productoRepository = productoRepository;
            _dbContext = dbContext;
            _imgService = imgService;
            _menuRepository = menuRepository;
            _etiquetaRepository = etiquetaRepository;
        }

        public async Task<RespuestaModel> RegistrarProductoCompleto(RegistroProductoDTO model)
        {
            //Procesado de imagen
            string? imagenUrl = await _imgService.ProcesarImg(model.ImagenFile);
            if (string.IsNullOrEmpty(imagenUrl))
                return RespuestasUtils.GenerarRespuesta(1, "Error al procesar la imagen del producto.");

            var productoDB = new NuevoProductoDTO(model, imagenUrl);

            // Interaccion con BD
            using var conexion = _dbContext.CrearConexionMySql();
            await conexion.OpenAsync();
            using var transaccion = await conexion.BeginTransactionAsync();

            try
            {
                // Registrar el producto principal
                var (codigoError, mensaje, idProducto) = await _productoRepository.RegistrarProducto(productoDB, conexion, transaccion);

                if (codigoError != 0)
                {
                    await transaccion.RollbackAsync();
                    return RespuestasUtils.GenerarRespuesta(codigoError, mensaje);
                }

                // Registrar presentaciones                
                foreach (var presentacion in model.Presentaciones)
                {
                    presentacion.IdProducto = idProducto;
                    var resultadoPresentacion = await _productoRepository.RegistrarPresentacionProducto(presentacion, conexion, transaccion);

                    if (resultadoPresentacion.codigoError > 0)
                    {
                        await transaccion.RollbackAsync();
                        return RespuestasUtils.GenerarRespuesta(resultadoPresentacion.codigoError, resultadoPresentacion.mensaje);
                    }
                }

                // Registrar etiquetas
                foreach (var etiqueta in model.Etiquetas)
                {
                    etiqueta.IdProducto = idProducto;
                    var resultadoEtiqueta = await _productoRepository.RegistraEtiquetaProducto(etiqueta, conexion, transaccion);

                    if (resultadoEtiqueta.codigoError > 0)
                    {
                        await transaccion.RollbackAsync();
                        return RespuestasUtils.GenerarRespuesta(resultadoEtiqueta.codigoError, resultadoEtiqueta.mensaje);
                    }
                }
                (codigoError, mensaje) = await _menuRepository.ActualizarVersionMenu();
                if (codigoError > 0)
                {
                    await transaccion.RollbackAsync();
                    return RespuestasUtils.GenerarRespuesta(codigoError, mensaje);
                }
                // Si todo fue exitoso, confirmar la transacción
                await transaccion.CommitAsync();    

                var numeroPresentaciones = model.Presentaciones.Count;
                return RespuestasUtils.GenerarRespuesta(0, $"Producto y {numeroPresentaciones} presentaciones registradas correctamente.");
            }
            catch (Exception ex)
            {
                await transaccion.RollbackAsync();
                throw;
            }            
        }
        public async Task<RespuestaModel> ActualizarProductoCompleto(ActualizarProductoDTO model)
        {
            if (model.ImagenFile != null)
            {
                // Procesar la nueva imagen
                string? imagenUrl = await _imgService.ProcesarImg(model.ImagenFile);
                if (string.IsNullOrEmpty(imagenUrl))
                    return RespuestasUtils.GenerarRespuesta(1, "Error al procesar la imagen del producto.");
                model.ImagenUrl = imagenUrl;
            }

            using var conexion = _dbContext.CrearConexionMySql();
            await conexion.OpenAsync();
            using var transaccion = await conexion.BeginTransactionAsync();

            try
            {
                // Actualizar el producto principal
                var productoDB = new ActualizarDatosProductoDTO(model);

                var (codigoError, mensaje) = await _productoRepository.ActualizarProducto(productoDB, conexion, transaccion);

                if (codigoError != 0)
                {
                    await transaccion.RollbackAsync();
                    return RespuestasUtils.GenerarRespuesta(codigoError, mensaje);
                }

                // Actualizar presentaciones                
                foreach (var presentacion in model.Presentaciones)
                {   
                    var resultadoPresentacion = await _productoRepository.ActualizarPresentacionProducto(presentacion, conexion, transaccion);

                    if (resultadoPresentacion.codigoError != 0)
                    {
                        await transaccion.RollbackAsync();
                        return RespuestasUtils.GenerarRespuesta(resultadoPresentacion.codigoError, resultadoPresentacion.mensaje);
                    }
                }

                // *** Eliminar etiquetas actuales ***
                var etiquetaDTO = new EliminarEtiquetasProductoDTO { IdProducto = model.IdProducto };
                (codigoError, mensaje) = await _etiquetaRepository.EliminarEtiquetasProducto(etiquetaDTO, conexion, transaccion);

                // Registrar etiquetas
                foreach (var etiqueta in model.Etiquetas)
                {
                    //etiqueta.IdProducto = idProducto;
                    var resultadoEtiqueta = await _productoRepository.RegistraEtiquetaProducto(etiqueta, conexion, transaccion);

                    if (resultadoEtiqueta.codigoError != 0)
                    {
                        await transaccion.RollbackAsync();
                        return RespuestasUtils.GenerarRespuesta(resultadoEtiqueta.codigoError, resultadoEtiqueta.mensaje);
                    }
                }
                (codigoError, mensaje) = await _menuRepository.ActualizarVersionMenu();
                if (codigoError != 0)
                {
                    await transaccion.RollbackAsync();
                    return RespuestasUtils.GenerarRespuesta(codigoError, mensaje);
                }
                // COMMIT
                await transaccion.CommitAsync();

                var numeroPresentaciones = model.Presentaciones.Count;
                return RespuestasUtils.GenerarRespuesta(0, $"Producto y {numeroPresentaciones} presentaciones registradas correctamente.");
            }
            catch (Exception ex)
            {
                await transaccion.RollbackAsync();
                throw;
            }
        }
    }
}