using SumoSushiApp_BackEnd.DTOs.Producto;
using SumoSushiApp_BackEnd.Models;

namespace SumoSushiApp_BackEnd.Services.Interfaces
{
    public interface IProductoService
    {
        public Task<RespuestaModel> RegistrarProductoCompleto(RegistroProductoDTO model);
        public Task<RespuestaModel> ActualizarProductoCompleto(ActualizarProductoDTO model);
    }
}
