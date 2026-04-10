<script lang="ts">
    import {link} from 'svelte-spa-router';  

    import { cantidadItems } from '../stores/CarritoStore';

    const {
        inicio = "",
        puntos = "",
        carrito = "",
        pedidos = "",
        perfil = ""
    }: {inicio?:string, puntos?:string, carrito?:string, pedidos?:string, perfil?:string} = $props();

    const totalCarrito = $derived(cantidadItems);

</script>

<div class="menu">
    <div class="menu__contenido">

        <a href="/" use:link class="icon" aria-label="Ir a inicio">
            <i class="fas fa-home {inicio}"></i>
        </a>   
        
        <a href="/misPuntos" use:link class="icon" aria-label="Ver mis puntos">
            <i class="fas fa-coins {puntos}"></i>
        </a> 
        
        <a href="/carrito" use:link class="icon menu__carrito" aria-label="Ir a mi carrito de compras">
            <i class="fas fa-shopping-cart {carrito}"></i>
            {#if $totalCarrito > 0}
                <p class="menu__badge">{$totalCarrito}</p>
            {/if}
        </a>   
        
        <a href="/seguimientoPedido" use:link class="icon" aria-label="Ir a mis pedidos">
            <i class="fas fa-receipt {pedidos}"></i>
        </a>   
        
        <a href="/perfilUsuario" use:link class="icon" aria-label="Ir a mi perfil">
            <i class="fas fa-id-badge {perfil}"></i>
        </a> 

    </div>
</div>

<style>
    .menu {
        background-color: var(--color-fondo-secundario);
        height: clamp(3rem, 8vh, 10rem);
        position: fixed;
        bottom: 0;
        left: 0;
        right: 0;
        border-radius: var(--border-radius) var(--border-radius) 0 0;
        z-index: 5;
    }

    .menu__contenido {
        padding: 0.2vh 3vw;
        display: flex;
        justify-content: space-between;
        height: 100%;  
        align-items: center;      
    }

    .iconoActivo {
        color: var(--color-principal);
    }

    .menu__carrito {
        position: relative;
    }

    .menu__badge {
        position: absolute;
        top: -15px;
        right: -10px;
        background-color: var(--color-principal);
        color: white;
        border-radius: 50%;
        padding: 0 5px;
    }
</style>
