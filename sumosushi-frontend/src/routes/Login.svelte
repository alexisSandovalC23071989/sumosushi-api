<script lang="ts">

    import Encabezado from '../components/Encabezado.svelte';
    import {link} from 'svelte-spa-router';
    import { inicioSesionService } from '../services/AuthService';
    import {LoginModel} from '../models/auth/LoginModel';
    import NavegacionInferior from '../components/NavegacionInferior.svelte';

    const usuario: LoginModel = new LoginModel();
    let iniciandoSesion: boolean = false;

    
    async function iniciarSesion(event: Event){
        event.preventDefault();
        iniciandoSesion = true;
        await inicioSesionService(usuario);
        iniciandoSesion = false;
    }
    
</script>
<Encabezado estadoRestaurante="Cerrado"/>
<section class="cont-main">    
    <article class="cont">
        
        <form onsubmit={iniciarSesion} class="form">
            <h1 class="txt-titulo">Inicio de sesión</h1>
            <input bind:value={usuario.correo} placeholder="Correo" class="input-txt" />
            <input bind:value={usuario.contrasenna} placeholder="Contraseña" type="password" class="input-txt" />            
            {#if !iniciandoSesion}
                <button type="submit" class="btn" >Iniciar Sesión</button>
            {:else}
                <button type="button" class="btn btn-oscuro" disabled>Procesando...</button>
            {/if}      
            <hr>
            <p class="txt-texto-claro">¿Olvidaste tu contraseña? <a href="/solicitarRecuperarContrasenna" use:link class="enlace">Recuperar contraseña</a></p>
            <hr>
            <p class="txt-texto-claro">¿Aun no tienes una cuenta? <a href="/registroUsuario" use:link class="enlace">Regístrate</a></p>
        </form>
        
    </article>    
</section>
<NavegacionInferior/>

<style>
    .enlace{
        color: var(--color-secundario)
    }
</style>
