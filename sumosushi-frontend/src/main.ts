import { mount } from 'svelte'
import App from './App.svelte'
import { registerSW } from 'virtual:pwa-register'
import { obtenerAccesToken } from './services/AuthService'
import { cargarMisPedidos } from './stores/MisPedidosStore'

const app = mount(App, {
  target: document.getElementById('app')!,
})

const updateSW = registerSW({
  onNeedRefresh() {console.log('Hay una nueva versión disponible. Recargá la app.');},
  onOfflineReady() {console.log('La app está lista para usar offline.');}
});

// Comunicacion con SW
navigator.serviceWorker.addEventListener('message', async (event) => {
  if (event.data?.tipo === 'actualizar-pedidos') {
    console.log('Mensaje del SW: actualizando pedidos...')
    const token = await obtenerAccesToken();
    if (!token) return;
    await cargarMisPedidos(token);
  }
})

export default app
