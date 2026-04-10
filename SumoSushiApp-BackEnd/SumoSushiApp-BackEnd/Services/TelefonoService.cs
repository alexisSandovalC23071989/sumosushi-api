using SumoSushiApp_BackEnd.DTOs.Usuario;
using SumoSushiApp_BackEnd.Models;
using SumoSushiApp_BackEnd.Repositories.Interfaces;
using SumoSushiApp_BackEnd.Services.Interfaces;
using SumoSushiApp_BackEnd.Services.Utils;

namespace SumoSushiApp_BackEnd.Services
{
    public class TelefonoService : ITelefonoService
    {
        private readonly ITelefonoRepository _telefonoRepository;
        public TelefonoService(ITelefonoRepository telefonoRepository)
        {
            _telefonoRepository = telefonoRepository;
        }

        public async Task<RespuestaModel> RegistrarTelefono(NuevoTelefonoUsuarioDTO model)
        {
            var (codigoError, mensaje) = await _telefonoRepository.RegistrarTelefono(model);
            return RespuestasUtils.GenerarRespuesta(codigoError, mensaje);
        }

        public async Task<RespuestaModel> EliminarTelefono(EliminarTelefonoUsuarioDTO model)
        {
            var (codigoError, mensaje) = await _telefonoRepository.EliminarTelefono(model);
            return RespuestasUtils.GenerarRespuesta(codigoError, mensaje);
        }
    }
}