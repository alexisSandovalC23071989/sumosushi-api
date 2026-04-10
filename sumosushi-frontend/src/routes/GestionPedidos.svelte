<script lang="ts">
  import { onMount } from 'svelte';
  import { iniciarRecepcionPedidos, activeConnection } from '../stores/SignalRStore';
  import Encabezado from '../components/Encabezado.svelte';
  import NavegacionInferior from '../components/NavegacionInferior.svelte';
  import GestionPedidoCard from '../components/GestionPedidoCard.svelte';
  import { audioHabilitado } from '../stores/AudioStore';   
  import { obtenerAccesToken } from '../services/AuthService';

  import { pedidos, cargarPedidos, hayPedidos} from "../stores/PedidosStore";

  // Acceso reactivo
  $: habilitado = $audioHabilitado;

  function activarAlertas() {
    audioHabilitado.inicializar();        
  }

  onMount(async () => {
    iniciarRecepcionPedidos((notificacion) => {});
    const token = await obtenerAccesToken();
    if (!token) return;
    await cargarPedidos(token);    
  });
</script>

<Encabezado estadoRestaurante="Cerrado"/>
<NavegacionInferior/>

<div class="cont-main">
  {#if !$activeConnection}
    <h2 class="txt-subtitulo">Error al conectar</h2>
  {/if}

  {#if $activeConnection}
    <h1 class="txt-titulo">Recibiendo pedidos...</h1>    
    <section class="cont">
      {#if !$audioHabilitado}
        <button class="btn" on:click={activarAlertas}>Activar alertas</button>
      {/if}      

      {#if !$hayPedidos}
        <h2 class="txt-subtitulo">No hay pedidos</h2>
      {/if}
      {#each $pedidos as pedido}
        <GestionPedidoCard pedido={pedido}/>
      {/each}
    </section>
  {/if}
</div>
