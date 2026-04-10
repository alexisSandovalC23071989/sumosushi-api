
const PUBLIC_VAPID_KEY = 'BKh3KH3UUQvXa9d65_4n2kEH2o4xpphdS4zDZBjizm26uiLjGC3qnZv6EvZyeiGvQSBgT62mwqbIhJy2oX7Xzys';

export async function suscribirseAPush(): Promise<PushSubscription | null> {
    if (!('serviceWorker' in navigator)) return null;
  
    const registration = await navigator.serviceWorker.ready;
  
    // Primero, buscar si ya hay una suscripción activa
    const existingSubscription = await registration.pushManager.getSubscription();
  
    if (existingSubscription) {
      // Si existe, desuscribirla para evitar conflictos
      await existingSubscription.unsubscribe();
      console.log("Subscripción previa eliminada");
    }
  
    // Ahora crear la nueva suscripción
    const subscription = await registration.pushManager.subscribe({
      userVisibleOnly: true,
      applicationServerKey: urlBase64ToUint8Array(PUBLIC_VAPID_KEY)
    });
  
    return subscription;
  }

function urlBase64ToUint8Array(base64String: string) {
  const padding = '='.repeat((4 - (base64String.length % 4)) % 4);
  const base64 = (base64String + padding)
    .replace(/-/g, '+')
    .replace(/_/g, '/');

  const rawData = window.atob(base64);
  return Uint8Array.from([...rawData].map(char => char.charCodeAt(0)));
}
