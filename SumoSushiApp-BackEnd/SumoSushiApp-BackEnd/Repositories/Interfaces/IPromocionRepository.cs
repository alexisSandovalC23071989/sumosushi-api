using SumoSushiApp_BackEnd.DTOs.Promocion;

namespace SumoSushiApp_BackEnd.Repositories.Interfaces
{
    public interface IPromocionRepository
    {
        Task<IEnumerable<PromocionDTO>> ObtenerTodasAsync();
        Task<IEnumerable<PromocionDTO>> ObtenerActivasAsync();
        Task<IEnumerable<PromocionDTO>> ObtenerInactivasAsync();
        Task<(int Codigo, string Mensaje)> CrearPromocionAsync(PromocionCrearDTO dto);
        Task<(int Codigo, string Mensaje)> ActualizarPromocionAsync(PromocionActualizarDTO dto);
        Task<(int Codigo, string Mensaje)> ActivarPromocionAsync(PromocionCambiarEstadoDTO dto);
        Task<(int Codigo, string Mensaje)> DeshabilitarPromocionAsync(PromocionCambiarEstadoDTO dto);
    }
}