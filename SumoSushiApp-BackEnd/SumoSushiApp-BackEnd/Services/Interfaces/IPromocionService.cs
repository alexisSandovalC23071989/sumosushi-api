using SumoSushiApp_BackEnd.DTOs.Promocion;
using SumoSushiApp_BackEnd.Models;

namespace SumoSushiApp_BackEnd.Services.Interfaces
{
    public interface IPromocionService
    {
        Task<RespuestaModel> ObtenerTodasPromociones();
        Task<RespuestaModel> ObtenerPromocionesActivas();
        Task<RespuestaModel> ObtenerPromocionesInactivas();
        Task<RespuestaModel> CrearPromocion(PromocionCrearDTO dto);
        Task<RespuestaModel> ActualizarPromocion(PromocionActualizarDTO dto);
        Task<RespuestaModel> ActivarPromocion(PromocionCambiarEstadoDTO dto);
        Task<RespuestaModel> DeshabilitarPromocion(PromocionCambiarEstadoDTO dto);
    }
}