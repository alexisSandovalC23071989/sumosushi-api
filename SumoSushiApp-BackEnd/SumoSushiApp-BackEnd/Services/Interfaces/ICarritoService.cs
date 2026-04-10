using SumoSushiApp_BackEnd.DTOs.Carrito;
using SumoSushiApp_BackEnd.Models;

namespace SumoSushiApp_BackEnd.Services.Interfaces
{
    public interface ICarritoService
    {
        public Task<RespuestaModel> AgregarOActualizarProducto(CarritoAgregarDTO dto);
        public Task<RespuestaModel> ObtenerCarrito(long idUsuario);

        //public Task<RespuestaModel> EliminarProducto(CarritoEliminarDTO dto);
        //public Task<RespuestaModel> ObtenerCarritoPromociones(long idUsuario);
        //public Task<RespuestaModel> ObtenerCarritoPresentaciones(long idUsuario);
    }
}
