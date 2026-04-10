<script lang="ts">
    import Encabezado from '../components/Encabezado.svelte';
    import {link} from 'svelte-spa-router';
    import {RegistroUsuarioModel} from '../models/auth/RegistroUsuarioModel';
    import {registroUsuarioService} from '../services/UsuarioService';

    const usuario = new RegistroUsuarioModel();

    function registrarUsuario(event: Event) {
        event.preventDefault();              
        registroUsuarioService(usuario);
    }
</script>
<Encabezado estadoRestaurante="Cerrado"/>
<section class="cont-main">
    
    <article class="cont">
        <form onsubmit={registrarUsuario} class="form">
            <h1 class="txt-titulo">Registro de usuario</h1>
            <input 
                bind:value={usuario.nombre} 
                type="text" 
                placeholder="Nombre" 
                class="input-txt"
                required />
            <input 
                bind:value={usuario.apellidos} 
                type="text" 
                placeholder="Apellidos" 
                class="input-txt"
                required />
            <input 
                bind:value={usuario.telefono} 
                type="tel" 
                placeholder="Telefono" 
                class="input-txt"
                required />
            <input 
                bind:value={usuario.correo} 
                type="email" 
                placeholder="Correo" 
                class="input-txt"
                required 
            />
            <input 
                bind:value={usuario.contrasenna} 
                type="password" 
                placeholder="Contraseña" 
                class="input-txt"
                minlength="6"
                required 
            />
            <input 
                bind:value={usuario.confirmarContrasenna} 
                type="password" 
                placeholder="Confirmar Contraseña" 
                class="input-txt"
                minlength="6"
                required 
            />

            <button type="submit" class="btn">Registrarse</button>

            <hr>

            <p class="txt-texto-claro">¿Ya tienes una cuenta? <a href="/login" use:link class="inicioSesion">Iniciar Sesión</a></p>
        </form>
    </article>   

</section>

<style>
    .inicioSesion{
        color: var(--color-secundario)
    }
</style>

