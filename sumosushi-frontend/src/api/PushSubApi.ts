import { suscribirseAPush } from '../utils/push';
import { API_BASE } from '../Config';

export async function activarNotificacionesApi(token: string) {
  const subscription = await suscribirseAPush();

  if (subscription) {
    console.log(JSON.stringify(subscription));

    // Extraer los valores manualmente
    const body = {
      endpoint: subscription.endpoint,
      p256dh: subscription.toJSON().keys?.p256dh,
      auth: subscription.toJSON().keys?.auth
    };

    await fetch(`${API_BASE}/notificaciones/suscribirse`, {
      method: 'POST',
      headers: { 
        'Content-Type': 'application/json', 
        'Authorization': `Bearer ${token}` 
      },
      body: JSON.stringify(body)
    });
  }
}