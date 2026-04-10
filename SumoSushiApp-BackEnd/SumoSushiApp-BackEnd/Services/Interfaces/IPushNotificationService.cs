using MySqlX.XDevAPI;
using SumoSushiApp_BackEnd.Models;
using WebPush;

namespace SumoSushiApp_BackEnd.Services.Interfaces
{
    public interface IPushNotificationService
    {
        public void AgregarSuscripcion(long idUsuario, PushSubscriptionModel subscription);

        public PushSubscriptionModel? ObtenerSuscripcion(long idUsuario);

        public Task EnviarNotificacionAsync(PushSubscriptionModel subscription, string title, string body, string url);

        public Task EnviarNotificacionAUsuarioAsync(long idUsuario, string title, string body, string url);
    }
}
