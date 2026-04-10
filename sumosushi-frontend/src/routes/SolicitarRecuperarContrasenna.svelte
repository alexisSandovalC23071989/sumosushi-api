<script lang="ts">

    import Encabezado from '../components/Encabezado.svelte';
    import {link} from 'svelte-spa-router';

    import {SolicitudRecuperacionContrasennaModel} from '../models/auth/SolicitudRecuperacionContrasennaModel';

    import {solicitudRecuperacionContrasennaService} from '../services/AuthService';

    const datosFormulario = new SolicitudRecuperacionContrasennaModel();

    function solicitarRecuperarContrasenna(event: Event){
        event.preventDefault();
        solicitudRecuperacionContrasennaService(datosFormulario);
    }
    
</script>

<section class="cont-main">    
    <article class="cont">
        <Encabezado estadoRestaurante="Cerrado"/>
        <form onsubmit={solicitarRecuperarContrasenna} class="form">
            <h1 class="txt-titulo">Recuperación de contraseña</h1>
            <input bind:value={datosFormulario.correo} placeholder="Correo" class="input-txt" />
            <button type="submit" class="btn">Enviar solicitud</button>        
        <hr>
        <p class="txt-texto-claro">Regresar a <a href="/login" use:link class="enlace">Inicio de sesión</a></p>
        </form>
    </article>
    
</section>

<style>
    .enlace{
        color: var(--color-secundario)
    }
</style>
