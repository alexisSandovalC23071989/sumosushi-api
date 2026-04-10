<script lang="ts">
    import { onMount } from "svelte";

    //Servicios
    import { obtenerAccesToken } from "../services/AuthService";
    import { actualizarEstadoCategoriaService, crearCategoriaService } from "../services/CategoriaService";

    //Stores
    import { 
        categorias, 
        categoriasVacios, 
        cargarCategorias } from "../stores/CategoriaStore";
    
    //Modelos
    import type { GestionCategoriaModel } from "../models/menu/GestionCategoriaModel";
    import { NuevaCategoriaModel } from "../models/menu/NuevaCategoriaModel";
 
    let creandoCategoria: boolean = $state(false);
    let nuevaCategoria = $state({Descripcion : ""}as NuevaCategoriaModel);

    onMount(async () => {
        if(categoriasVacios()){
            const token = await obtenerAccesToken();
            if (!token) return;
            await cargarCategorias(token);
        }
    });

    async function actualizarEstadoCategoria(categoria: GestionCategoriaModel) {
        const token = await obtenerAccesToken();
        if (!token) return;
        await actualizarEstadoCategoriaService(token, categoria); 
    }
    async function crearCategoria(){
        const token = await obtenerAccesToken();
        if (!token) return;
        await crearCategoriaService(token, nuevaCategoria); 
    }
</script>

<table>
    <thead>
        <tr>
            <th>Descripcion</th>
            <th class="td-centrado">Estado</th>
        </tr>
    </thead>
    <tbody>
        {#if $categorias.length === 0}
            <tr>
                <td colspan="2" class="td-centrado">No hay categorías</td>
            </tr>
        {/if}
        {#each $categorias as categoria}
        <tr>
            <td>{categoria.Descripcion}</td>
            <td class="td-centrado">
                <input type="checkbox" 
                    class="input-checkbox" 
                    bind:checked={categoria.EstadoBool}
                    onchange={() => actualizarEstadoCategoria(categoria)} />
            </td>
        </tr>
        {/each}
    </tbody>
</table>

{#if creandoCategoria}
    <h2 class="txt-subtitulo">Creando nueva categoría</h2>
    <input type="text" 
        class="input-txt" 
        bind:value={nuevaCategoria.Descripcion} 
        placeholder="Nombre de la categoria"/>
    <button class="btn btn-oscuro" onclick={() => creandoCategoria = false}>Cancelar</button>
    <button class="btn" onclick={crearCategoria}>Guardar</button>
{:else}
    <button class="btn" onclick={() => creandoCategoria = true}>Nueva categoria</button>
{/if}


<style>


    
</style>