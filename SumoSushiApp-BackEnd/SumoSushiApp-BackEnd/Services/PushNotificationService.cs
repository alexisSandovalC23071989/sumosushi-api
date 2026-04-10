using SumoSushiApp_BackEnd.Models;
using SumoSushiApp_BackEnd.Services.Interfaces;
using System.Collections.Concurrent;
using System.Text.Json;
using System.Threading.Tasks;
using WebPush;

namespace SumoSushiApp_BackEnd.Services
{
    public class PushNotificationService : IPushNotificationService
    {
        private readonly VapidDetails _vapidDetails;
        private readonly WebPushClient _client;

        // Diccionario concurrente para manejar suscripciones por usuario
        private static readonly ConcurrentDictionary<long, PushSubscriptionModel> Subscripciones = new();

        public PushNotificationService(IConfiguration configuration)
        {
            _client = new WebPushClient();

            _vapidDetails = new VapidDetails(
                "mailto:sumosushi@gmail.com",
                configuration["VAPID:PublicKey"]!,
                configuration["VAPID:PrivateKey"]!
            );
        }

        public void AgregarSuscripcion(long idUsuario, PushSubscriptionModel subscription)
        {
            Subscripciones[idUsuario] = subscription;
        }

        public PushSubscriptionModel? ObtenerSuscripcion(long idUsuario)
        {
            Subscripciones.TryGetValue(idUsuario, out var sub);
            return sub;
        }

        public async Task EnviarNotificacionAsync(PushSubscriptionModel subscription, string title, string body, string url)
        {
            var pushSubscription = new PushSubscription(
                subscription.Endpoint,
                subscription.P256DH,
                subscription.Auth
            );

            var payload = JsonSerializer.Serialize(new { title, body, url });

            try
            {
                await _client.SendNotificationAsync(pushSubscription, payload, _vapidDetails);
            }
            catch
            {
                // Ignorar cualquier error al enviar la notificación
            }
        }

        public async Task EnviarNotificacionAUsuarioAsync(long idUsuario, string title, string body, string url)
        {
            var sub = ObtenerSuscripcion(idUsuario);
            if (sub != null)
            {
                await EnviarNotificacionAsync(sub, title, body, url);
            }
            // Si no hay suscripción, no se hace nada
        }
    }
}
