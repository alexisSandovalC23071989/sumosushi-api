<script lang="ts">

import Encabezado from '../components/Encabezado.svelte';
  import MiPedidoCard from '../components/MiPedidoCard.svelte';
import NavegacionInferior from '../components/NavegacionInferior.svelte';
import { obtenerAccesToken } from '../services/AuthService';
import { misPedidos, cargarMisPedidos } from '../stores/MisPedidosStore';
import { onMount } from 'svelte';

onMount(async () => {
    const token = await obtenerAccesToken();
    if (!token) return;
    await cargarMisPedidos(token);
});


</script>

<Encabezado estadoRestaurante="Cerrado"/>

<div class="cont-main">
    <div class="cont">
        <h1 class="txt-titulo">Seguimiento de pedido</h1>
        {#each $misPedidos as pedido}
            <MiPedidoCard pedido={pedido}/>
        {/each}
    </div>
</div>

<NavegacionInferior pedidos="iconoActivo"/>