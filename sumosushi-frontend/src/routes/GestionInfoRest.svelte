<script lang="ts">
    import Encabezado from '../components/Encabezado.svelte';
    import {InfoRestauranteModel} from '../models/restaurante/InfoRestauranteModel';
    import {actualizarInfoRestauranteService, obtenerInfoRestauranteService} from '../services/RestauranteService';
    import {onMount} from 'svelte';
    import {infoSesion} from '../stores/AuthStores';
    import { refreshAccesTokenService } from '../services/AuthService';
    import NavegacionInferior from '../components/NavegacionInferior.svelte';

    let infoRestaurante: InfoRestauranteModel = new InfoRestauranteModel();
    let editando = false;

    let infoRestauranteForm: InfoRestauranteModel;

    function activarEdicion() {
        infoRestauranteForm = new InfoRestauranteModel();
        infoRestauranteForm.LogoUrl = infoRestaurante.LogoUrl;
        infoRestauranteForm.Nombre = infoRestaurante.Nombre;
        infoRestauranteForm.Direccion = infoRestaurante.Direccion;
        infoRestauranteForm.Telefono = infoRestaurante.Telefono;
        infoRestauranteForm.Correo = infoRestaurante.Correo;
        editando = true;
    }

    function cancelarEdicion() {
        editando = false;
    }

    async function actualizarInfoRestaurante() {
        if ($infoSesion.esTokenValido){
            console.log("Se activo refresh");
            await refreshAccesTokenService();
        }
        await actualizarInfoRestauranteService(infoRestauranteForm);
    }

    onMount(async () => {
        if (!$infoSesion.esTokenValido){
            await refreshAccesTokenService();
        }
        infoRestaurante = await obtenerInfoRestauranteService();
    });
</script>

<Encabezado estadoRestaurante="Cerrado"/>

<section class="cont-main">
    <article class="cont">
        {#if !editando}
            <div class="cont-interno-centrado">
                <h1 class="txt-titulo">Información del restaurante</h1>
                <div class="img-cont">
                    <img src="{infoRestaurante.LogoUrl}" alt="Logo" class="logo txt-texto"/>
                </div>
            </div>
            <p class="txt-texto"><strong>Nombre:</strong> {infoRestaurante.Nombre}</p>
            <p class="txt-texto"><strong>Dirección:</strong> {infoRestaurante.Direccion}</p>
            <p class="txt-texto"><strong>Teléfono:</strong> {infoRestaurante.Telefono}</p>
            <p class="txt-texto"><strong>Correo:</strong> {infoRestaurante.Correo}</p> 
            <button class="btn" onclick={activarEdicion}>{editando ? 'Cancelar' : 'Editar'}</button>
        {/if} 
        {#if editando}
            <div class="cont-interno-centrado">
                <h1 class="txt-titulo">Editando información</h1>                
            </div>
            <input class="input-txt" type="text" bind:value={infoRestauranteForm.LogoUrl} placeholder="LogoUrl">
            <input class="input-txt" type="text" bind:value={infoRestauranteForm.Nombre} placeholder="Nombre"/>
            <input class="input-txt" type="text" bind:value={infoRestauranteForm.Direccion} placeholder="Dirección"/>
            <input class="input-txt" type="text" bind:value={infoRestauranteForm.Telefono} placeholder="Teléfono"/>
            <input class="input-txt" type="text" bind:value={infoRestauranteForm.Correo} placeholder="Correo"/>
            <div class="cont-fila">                
                <button class="btn btn-oscuro" onclick={cancelarEdicion}>Cancelar</button>
                <button class="btn" onclick={actualizarInfoRestaurante}>Guardar</button>
            </div>
        {/if} 
             
    </article>
</section>

<NavegacionInferior/>

<style>
    .logo {
        width: 200px;
        height: 200px;
        object-fit: contain; 
        display: block;
    } 
    .img-cont{
        display: flex;
        justify-content: center;
    }
</style>
