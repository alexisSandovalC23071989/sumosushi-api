<script lang="ts">

    //Componentes
    import Encabezado from '../components/Encabezado.svelte';
    import CategoriaCard from '../components/CategoriaCard.svelte';
    import NavegacionInferior from '../components/NavegacionInferior.svelte';
    import GestionPromociones from '../components/GestionPromociones.svelte';
    import GestionProductos from '../components/GestioProductos.svelte';

    import { onMount } from 'svelte';
    import { obtenerAccesToken } from '../services/AuthService';
    import { cargarCategorias } from '../stores/CategoriaStore';
    import { cargarEtiquetas } from '../stores/EtiquetasStore';

    let seleccionado = $state('1');

    // onMount(async () => {
    //     const token = await obtenerAccesToken();
    //     if (!token) return;
    //     await cargarCategorias(token);
    //     await cargarEtiquetas(token);
    // });



</script>
<Encabezado estadoRestaurante="Cerrado"/>   
<section class="cont-main">
    <div class="cont">

        <h1 class="txt-titulo">Gestionar Menu</h1>
        <select class="input-txt" bind:value={seleccionado} id="gestion-menu">
            <option value="1">Categorías</option>
            <option value="2">Productos</option>
            <option value="3">Promociones</option>
        </select>
        <hr>
        {#if seleccionado === '1'}
        <h2 class="txt-subtitulo">Categorias</h2>
        <CategoriaCard/>
        {/if}
        {#if seleccionado === '2'}
        <GestionProductos/>
        {/if}
        {#if seleccionado === '3'}
        <GestionPromociones/>
        {/if}        
    </div>    
</section>
<NavegacionInferior/>
