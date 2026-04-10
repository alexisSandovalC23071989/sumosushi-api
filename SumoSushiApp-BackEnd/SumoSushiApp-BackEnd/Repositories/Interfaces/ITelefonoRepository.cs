using SumoSushiApp_BackEnd.DTOs.Usuario;

namespace SumoSushiApp_BackEnd.Repositories.Interfaces
{
    public interface ITelefonoRepository
    {
        public Task<IEnumerable<string?>> ObtenerTelefonosUsuario(long idUsuario);
        public Task<(int codigoError, string mensaje)> RegistrarTelefono(NuevoTelefonoUsuarioDTO model);
        public Task<(int codigoError, string mensaje)> EliminarTelefono(EliminarTelefonoUsuarioDTO model);
    }
}
