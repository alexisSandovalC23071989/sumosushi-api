<script lang="ts">
    import Encabezado from '../components/Encabezado.svelte';
    import NavegacionInferior from '../components/NavegacionInferior.svelte';
    import { activarNotificacionesApi } from '../api/PushSubApi';
    import { obtenerAccesToken } from '../services/AuthService';

    async function activarNotificaciones() {
        const token = await obtenerAccesToken();
        if (!token) return;
        await activarNotificacionesApi(token);
    }
</script>

<Encabezado estadoRestaurante="Cerrado"/>

<div class="cont-main">
    <h1 class="txt-titulo">Mis puntos</h1>
    <button class="btn" onclick={activarNotificaciones}>Subscribirse a push</button>
</div>

<NavegacionInferior puntos="iconoActivo"/>