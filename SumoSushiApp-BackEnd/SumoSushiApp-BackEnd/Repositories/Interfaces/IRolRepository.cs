using SumoSushiApp_BackEnd.DTOs.Rol;

namespace SumoSushiApp_BackEnd.Repositories.Interfaces
{
    public interface IRolRepository
    {
        public Task<IEnumerable<RolDTO>> ObtenerRoles();
    }
}
