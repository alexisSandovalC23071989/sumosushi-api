using SumoSushiApp_BackEnd.DTOs.Auth;

namespace SumoSushiApp_BackEnd.Services.Interfaces
{
    public interface ICorreoService
    {
        public Task<bool> EnviarCorreo(string toEmail, string subject, string body, bool isHtml = false);
        public Task<string> GenerarCorreo(AuthTokenRecuperacionDTO model);
    }
}
