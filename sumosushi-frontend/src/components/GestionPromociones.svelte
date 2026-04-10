<script lang="ts">

    import { onMount } from "svelte";

    //Stores
    import { promociones, estaPromocionesVacio, cargarPromociones } from "../stores/PromocionesStore";

    //Componentes
    import PromocionCard from "./PromocionCard.svelte";
    import ModalNuevaPromocion from "./ModalNuevaPromocion.svelte";

    //Servicios
    import { obtenerAccesToken } from "../services/AuthService";

    let registrandoPromocion = $state(false);

    onMount(async () => {
        if (estaPromocionesVacio()) {
            const token = await obtenerAccesToken();
            if (!token) return;
            await cargarPromociones(token);
        }
    });

    function ocultarModal() {
        registrandoPromocion = false;
    };

</script>
<div class="cont-fila">
    <h1 class="txt-titulo">Gestionar Promociones</h1>
    <button aria-label="Agregar promocion" class="btn-icon" onclick={() => registrandoPromocion = true}>
        <i class="fas fa-circle-plus icon"></i>
      </button>
</div>

    
    {#if $promociones.length === 0}
        <p>No hay promociones</p>
    {/if}
    {#each $promociones as promocion}
    <PromocionCard promocion={promocion} />
    {/each}

    {#if registrandoPromocion}
        <ModalNuevaPromocion cerrarModal={ocultarModal} />
    {/if}
