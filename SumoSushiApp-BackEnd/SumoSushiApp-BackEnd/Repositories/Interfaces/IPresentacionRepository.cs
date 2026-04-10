using SumoSushiApp_BackEnd.DTOs.Presentacion;

namespace SumoSushiApp_BackEnd.Repositories.Interfaces
{
    public interface IPresentacionRepository
    {
        public Task<IEnumerable<PresentacionDTO>> ObtenerEtiquetas();
    }
}
