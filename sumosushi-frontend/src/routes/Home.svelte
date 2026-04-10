<script lang="ts"> 
    
    // UTILS    
    import { menu, cargarMenu, estaMenuVacio } from '../stores/MenuStore';
    import { onMount } from 'svelte';


    // COMPONENTES
    import Encabezado from '../components/Encabezado.svelte';
    import NavegacionInferior from '../components/NavegacionInferior.svelte';
    import MenuCategorias from '../components/MenuCategorias.svelte';
    import PromocionesMenu from '../components/MenuPromociones.svelte';
    import ModalProductoSeleccionado from '../components/ModalProductoSeleccionado.svelte';
    import ProductosLista from '../components/ProductosLista.svelte';
    import type { ProductoMenuModel } from '../models/menu/ProductoMenuModel';
    import { cargarCarrito, estaCarritoVacio } from '../stores/CarritoStore';
    import { obtenerAccesToken } from '../services/AuthService';
    import { infoSesion } from '../stores/AuthStores';

    let productoSeleccionado: ProductoMenuModel | null = null;

    function mostrarDetalle(producto: ProductoMenuModel) {
        productoSeleccionado = producto;
    }

    onMount(async () => {        
        if (estaMenuVacio()) {
            await cargarMenu();
        }
        if($infoSesion.estaAutenticado && estaCarritoVacio()){
            var token = await obtenerAccesToken();
            if (!token) return;
            await cargarCarrito(token);
        }
    });

</script>
<Encabezado estadoRestaurante="Cerrado"/>
<NavegacionInferior inicio="iconoActivo"/>

<div class="cont-main home">

<MenuCategorias/>

<PromocionesMenu/>

<!-- <MenuProductos mostrarDetalle={mostrarDetalle}/> -->
<ProductosLista categorias={$menu.Categorias} mostrarDetalle={mostrarDetalle} />

<ModalProductoSeleccionado productoSeleccionado={productoSeleccionado}/>

</div>

