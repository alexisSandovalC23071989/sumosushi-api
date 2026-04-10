<script lang="ts">

    import Encabezado from '../components/Encabezado.svelte';
    import {link} from 'svelte-spa-router';

    import {RecuperacionContrasennaModel} from '../models/auth/RecuperacionContrasennaModel';

    import {recuperacionContrasennaService} from '../services/AuthService';

    const datosFormulario = new RecuperacionContrasennaModel();

    function recuperarContrasenna(event: Event){
        event.preventDefault();
        recuperacionContrasennaService(datosFormulario);
    }
    
</script>

<section class="cont-main">    
    <article class="cont">
        <Encabezado estadoRestaurante="Cerrado"/>
        <form onsubmit={recuperarContrasenna} class="form">
            <h1 class="txt-titulo">Recuperación de contraseña</h1>
            <input bind:value={datosFormulario.correo} placeholder="Correo" class="input-txt" />
            <input bind:value={datosFormulario.token} placeholder="Token" class="input-txt" />
            <input bind:value={datosFormulario.contrasenna} placeholder="Contraseña" type="password" class="input-txt" />
            <input bind:value={datosFormulario.confirmarContrasenna} placeholder="Confirmación contraseña" type="password" class="input-txt" />

            <button type="submit" class="btn">Restablecer contraseña</button>        
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
