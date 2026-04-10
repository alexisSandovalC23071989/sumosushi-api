<script lang="ts"> 
    import {link} from 'svelte-spa-router';  
    import { infoSesion } from '../stores/AuthStores';
    import { cerrarSesionService } from '../services/AuthService';
    import { audioHabilitado } from '../stores/AudioStore';
    let {estadoRestaurante = "Cerrado"}: {estadoRestaurante?:string} = $props();   

    let menuActivo: "menuInactivo" | "menuActivo" = $state("menuInactivo");

    function toggleMenu(){
        menuActivo = menuActivo === "menuInactivo" ? "menuActivo" : "menuInactivo";
    }     

    function cerrarSesion(){
        const decision = confirm("¿Estas seguro de cerrar sesión?");
        if (decision){
            cerrarSesionService();
        }
    }

    function activarAlertas() {
        audioHabilitado.inicializar();        
    }

</script>

<div class="encabezado">
    <div class="encabezado__contenido">
        <h3 class="encabezado__estado txt-subtitulo">{estadoRestaurante}</h3>
        <img class="encabezado__logo" src="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS-8dPoM5b2nf3SwoIaOz6wG0TnS4DoPiKW0g&s" alt="Logo"/>        
        <button class="encabezado__boton" onclick={toggleMenu} aria-label="Abrir menú">
            <svg
                xmlns="http://www.w3.org/2000/svg"
                fill="none"
                viewBox="0 0 24 24"
                stroke-width="2"
                stroke="currentColor"
                class="icon"
                >
                <path
                    stroke-linecap="round"
                    stroke-linejoin="round"
                    d="M17.982 18.725A7.488 7.488 0 0 0 12 15.75a7.488 7.488 0 0 0-5.982 2.975m11.963 0a9 9 0 1 0-11.963 0m11.963 0A8.966 8.966 0 0 1 12 21a8.966 8.966 0 0 1-5.982-2.275M15 9.75a3 3 0 1 1-6 0 3 3 0 0 1 6 0Z" />
            </svg>
        </button>        
    </div>        

    <div class={menuActivo}>
        {#if $infoSesion.estaAutenticado}
            <button onclick={cerrarSesion} class="txt-enlace">Cerrar sesión</button>
        {:else}
            <a href="/login" use:link class="txt-enlace">Iniciar Sesión</a>
        {/if}

        {#if $infoSesion.Rol === "Admin" || $infoSesion.Rol === "Colaborador"}
            <hr>
            <a href="/gestionPedidos" use:link class="txt-enlace" onclick={activarAlertas}>Gestión Pedidos</a>
        {/if}

        {#if $infoSesion.Rol === "Admin"}
            <hr>
            <a href="/gestionHorario" use:link class="txt-enlace">Gestión Horarios</a>
            <a href="/gestionInfoRest" use:link class="txt-enlace">Gestión Información Restaurante</a>
            <a href="/gestionCuentas" use:link class="txt-enlace">Gestión Cuentas</a>
            <hr>
            <a href="/gestionMenu" use:link class="txt-enlace">Gestión Menú</a>
            
            <!--                         
            <a href="/" use:link class="txt-enlace">Puntos</a>
            <a href="/" use:link class="txt-enlace">Metricas</a>
            -->
        {/if}
    </div>
</div>

<style>
    .encabezado{
        background-color: var(--color-principal);
        height: 75px;        
        position: fixed;
        top: 0; 
        width:100%;
        position: fixed;
        z-index: 10;

        display: flex;
        justify-content: center;
    }    
    .encabezado__contenido{        
        width: 90%;
        display: flex;
        justify-content: space-between;
        align-items: flex-end;
        height: 100%;        
    }
    .encabezado__estado, .encabezado__boton{
        margin-bottom: 10px;
        background-color: transparent;
    }
    .encabezado__logo{
        position: absolute;
        left: 50%;
        transform: translateX(-50%);
        max-height: 100%;
        width: auto;
    }
    .menuInactivo{
        display: none;
    }
    .menuActivo{
        display: block;     
        position: absolute;
        top: 99%; /* Lo posiciona justo debajo del encabezado */
           
        padding: 1vh 2vw;
        right: 0;
        background-color: var(--color-principal);
        width: auto;     
        
        display: flex;
        flex-direction: column;
        gap:10px;

        z-index: 10;

        border-bottom-left-radius: var(--border-radius);
    }
    .menuActivo a{
        font-weight: bold;
    }

    .icon {
    width: var(--tamanio-titulos);
    height: var(--tamanio-titulos);
    color: var(--color-texto-principal);
    transition: color 0.3s;
  }

</style>
