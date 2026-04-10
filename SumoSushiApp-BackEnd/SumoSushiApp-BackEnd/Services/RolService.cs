using SumoSushiApp_BackEnd.DTOs.Rol;
using SumoSushiApp_BackEnd.Models;
using SumoSushiApp_BackEnd.Repositories.Interfaces;
using SumoSushiApp_BackEnd.Services.Interfaces;
using SumoSushiApp_BackEnd.Services.Utils;

namespace SumoSushiApp_BackEnd.Services
{
    public class RolService : IRolService
    {
        private readonly IRolRepository _rolRepository;
        public RolService(IRolRepository rolRepository)
        {
            _rolRepository = rolRepository;
        }
        public async Task<RespuestaModel> ObtenerRoles()
        {
            var roles = await _rolRepository.ObtenerRoles();
            return RespuestasUtils.GenerarRespuesta(roles);
        }
    }
}
