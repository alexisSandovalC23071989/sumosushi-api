

import { precacheAndRoute } from 'workbox-precaching';

// Precache de los assets generados
precacheAndRoute(self.__WB_MANIFEST || [])

// --- Tu código push notification ---

self.addEventListener('push', function (event) {
  const data = event.data?.json() || {}

  const title = data.title || 'Notificación'
  const options = {
    body: data.body || '',
    icon: '/icon-192x192.png',
    data: data.url || '/'
  }

  event.waitUntil(
    (async () => {
      // Mostrar la notificación
      await self.registration.showNotification(title, options)

      // Enviar mensaje a los clientes (tabs/pestañas) abiertos
      const allClients = await self.clients.matchAll({ includeUncontrolled: true })
      for (const client of allClients) {
        client.postMessage({ tipo: 'actualizar-pedidos' })
      }
    })()
  )
})

self.addEventListener('notificationclick', function (event) {
  event.notification.close()
  event.waitUntil(
    clients.openWindow(event.notification.data)
  )
})
