using Dapper;
using Google.Protobuf.WellKnownTypes;
using SumoSushiApp_BackEnd.Data;
using SumoSushiApp_BackEnd.DTOs.Carrito;
using SumoSushiApp_BackEnd.Models;
using SumoSushiApp_BackEnd.Repositories.Interfaces;
using SumoSushiApp_BackEnd.Services.Interfaces;
using SumoSushiApp_BackEnd.Services.Utils;
using System.Data;

namespace SumoSushiApp_BackEnd.Services
{

    public class CarritoService : ICarritoService
    {
        private readonly ICarritoRepository _repo;

        public CarritoService(ICarritoRepository repo)
        {
            _repo = repo;
        }

        public async Task<RespuestaModel> AgregarOActualizarProducto(CarritoAgregarDTO dto)
        {
            int codigo = 0;
            string mensaje = "";

            // Normalización de valores nulos
            if (dto.IdPresentacionProducto == 0)
                dto.IdPresentacionProducto = null;
            if (dto.IdPromocion == 0)
                dto.IdPromocion = null;

            // Validaciones: solo una de las dos debe tener valor
            bool tienePresentacion = dto.IdPresentacionProducto.HasValue;
            bool tienePromocion = dto.IdPromocion.HasValue;

            if (tienePresentacion && !tienePromocion)
            {
                (codigo, mensaje) = await _repo.AgregarOActualizarPresentacion(dto);
            }
            else if (tienePromocion && !tienePresentacion)
            {
                (codigo, mensaje) = await _repo.AgregarOActualizarPromocion(dto);
            }
            else
            {
                return RespuestasUtils.GenerarRespuesta(1, "Debe enviar solo uno entre IdPresentacionProducto o IdPromocion (no ambos o ninguno).");
            }

            return RespuestasUtils.GenerarRespuesta(codigo, mensaje);
        }

        //public async Task<RespuestaModel> EliminarProducto(CarritoEliminarDTO dto)
        //{
        //    var (codigo, mensaje) = await _repo.EliminarProducto(dto);
        //    return RespuestasUtils.GenerarRespuesta(codigo, mensaje);
        //}
        //Se obtiene los datos totales con idpromocion y idpresentacion.
        public async Task<RespuestaModel> ObtenerCarrito(long idUsuario)
        {
            var lista = await _repo.ObtenerCarrito(idUsuario);
            return RespuestasUtils.GenerarRespuesta(lista);
        }

        //Se obtienen los datos unicamente de idpromocion.

        // Se obtienen los datos únicamente de IdPromocion
        //public async Task<RespuestaModel> ObtenerCarritoPromociones(long idUsuario)
        //{
        //    var lista = await _repo.ObtenerCarrito(idUsuario);

        //    var promociones = lista
        //        .Where(item => item.IdPromocion.HasValue)
        //        .Select(item => new ObtenerCarritoPromocionDTO
        //        {
        //            IdPromocion = item.IdPromocion.Value,
        //            IdTipoEntidad = item.IdTipoEntidad,
        //            Cantidad = item.Cantidad
        //        })
        //        .ToList();

        //    return RespuestasUtils.GenerarRespuesta(promociones);
        //}



        //Se obtienen los datos unicamente de idpresentaciones.
        //public async Task<RespuestaModel> ObtenerCarritoPresentaciones(long idUsuario)
        //{
        //    var lista = await _repo.ObtenerCarrito(idUsuario);

        //    var presentaciones = lista
        //        .Where(item => item.IdPresentacionProducto.HasValue)
        //        .Select(item => new ObtenerCarritoPresentacionDTO
        //        {
        //            IdPresentacionProducto = item.IdPresentacionProducto.Value,
        //            IdTipoEntidad = item.IdTipoEntidad,
        //            Cantidad = item.Cantidad
        //        })
        //        .ToList();

        //    return RespuestasUtils.GenerarRespuesta(presentaciones);
        //}




    }

}
