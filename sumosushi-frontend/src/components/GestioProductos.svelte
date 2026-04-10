<script lang="ts"> 

import {onMount} from "svelte";

//Stores
import {cargarProductos, estaProductosVacio, getProductosPorEstado, productos} from "../stores/ProductosStore";
import {cargarCategorias} from "../stores/CategoriaStore";
import {cargarEtiquetas} from "../stores/EtiquetasStore";
import {cargarPresentaciones} from "../stores/PresentacionStore";
//Servicios
import {obtenerAccesToken} from "../services/AuthService";
//Componentes
import CardGestionProducto from "./CardGestionProducto.svelte";
import ModalNuevoProducto from "./ModalNuevoProducto.svelte";
import type { ProductoCompletoModel } from "../models/menu/ProductoCompletoModel";
import { get } from "svelte/store";

let mostrandoProductos: Array<ProductoCompletoModel> = $state(get(productos));
let registrandoProducto = $state(false);
let cargado:boolean = $state(false); 
let filtro = $state('');

onMount(async () => {
    if (estaProductosVacio()) {
        const token = await obtenerAccesToken();
        if (!token) return;
        await cargarProductos(token);
        await cargarCategorias(token);
        await cargarEtiquetas(token);
        await cargarPresentaciones(token);
        mostrandoProductos = getProductosPorEstado(filtro);        
    }
    cargado = true;
});

function filtrarProductos() {
    mostrandoProductos = getProductosPorEstado(filtro);
}

function ocultarModal() {
    registrandoProducto = false;
};

</script>

<article class="cont-fila">
    <h1 class="txt-titulo">Gestión de productos</h1>
    <button aria-label="Agregar producto" class="btn-icon" onclick={() => registrandoProducto = true}>
        <i class="fas fa-circle-plus icon"></i>
      </button>
</article>



{#if !cargado}
    <p class="txt-subtitulo">Cargando productos...</p>
{/if}

{#if cargado}
    <article class="cont-interno-centrado" id="filtrarProductos">
        <select class="input-txt" bind:value={filtro} onchange={filtrarProductos} id="filtro">
            <option value="">Todos...</option>
            <option value="Activo">Activos</option>
            <option value="Inactivo">Inactivos</option>
            <option value="Agotado">Agotados</option>
        </select>
    </article>


    <section class="cont-interno-justificado">
    {#if mostrandoProductos.length === 0}
        {#if filtro === ''}
            <p class="txt-texto">El menú aún no tiene productos</p>
        {/if}
        {#if filtro === 'Activo'}
            <p class="txt-texto">No hay productos activos</p>
        {/if}
        {#if filtro === 'Inactivo'}
            <p class="txt-texto">No hay productos inactivos</p>
        {/if}
        {#if filtro === 'Agotado'}
            <p class="txt-texto">No hay productos agotados</p>
        {/if}
    {/if}
    {#each mostrandoProductos as producto}
        <CardGestionProducto producto={producto} />
    {/each}
    </section>
{/if}

{#if registrandoProducto}
    <ModalNuevoProducto cerrarModal={ocultarModal} />
{/if}
