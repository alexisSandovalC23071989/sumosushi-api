<script lang="ts">
    import Encabezado from "../components/Encabezado.svelte";
    import NavegacionInferior from "../components/NavegacionInferior.svelte";
    import ModalProductoSeleccionado from "../components/ModalProductoSeleccionado.svelte";
    import ProductosLista from "../components/ProductosLista.svelte";

    import {carrito, estaCarritoVacio} from "../stores/CarritoStore";
    import { cargarMenu, estaMenuVacio, menu } from "../stores/MenuStore";
    import { cargarCarrito } from "../stores/CarritoStore";
    import { obtenerAccesToken } from "../services/AuthService";
    import { ProductoMenuModel } from "../models/menu/ProductoMenuModel";
    import { onMount } from "svelte";
    import { infoSesion } from "../stores/AuthStores";
    import { goToLogin } from "./Routes";
    import { goToCheckOut } from "./Routes";

    let productoSeleccionado: ProductoMenuModel | null = null;

    $: totalCarrito = $carrito.reduce((total, item) => {
        const producto = $menu.Categorias
            .flatMap(c => c.Productos)
            .find(p => p.Presentaciones.some(pr => pr.IdPresentacionProducto === item.IdPresentacionProducto));

        const presentacion = producto?.Presentaciones.find(pr => pr.IdPresentacionProducto === item.IdPresentacionProducto);

        const subtotal = presentacion ? presentacion.Precio * item.Cantidad : 0;

        return total + subtotal;
    }, 0);

    $: categoriasCarrito = $menu.Categorias
    .map(categoria => {
        const productosFiltrados = categoria.Productos
            .map(producto => {
                const presentacionesFiltradas = producto.Presentaciones
                    .filter(p => $carrito.some(item => item.IdPresentacionProducto === p.IdPresentacionProducto));
                
                if (presentacionesFiltradas.length > 0) {
                    return {
                        ...producto,
                        Presentaciones: presentacionesFiltradas
                    };
                }
                return null;
            })
            .filter(p => p !== null) as ProductoMenuModel[];

        return productosFiltrados.length > 0
            ? { Categoria: categoria.Categoria, Productos: productosFiltrados }
            : null;
    })
    .filter(c => c !== null);

    onMount(async () => {
        if(!$infoSesion.estaAutenticado){
            goToLogin();
            return;
        }
        if (estaMenuVacio()) {
            await cargarMenu();
        }
        if(estaCarritoVacio()){
            var token = await obtenerAccesToken();
            if (!token) return;
            await cargarCarrito(token);
        }
    });
    

    function mostrarDetalle(producto: ProductoMenuModel) {
        productoSeleccionado = producto;
    }

    function CheckOut() {
        goToCheckOut();
    }

</script>

<Encabezado estadoRestaurante="Cerrado"/>

<div class="cont-main">
    <h1 class="txt-titulo">Productos en tu carrito</h1>
    {#if $infoSesion.estaAutenticado}    
        <ProductosLista categorias={categoriasCarrito} mostrarDetalle={mostrarDetalle} modoCarrito={true} />
        {#if totalCarrito > 0}
            <div class="total-carrito">

                <button class="btn" onclick={() => CheckOut()}> Continuar compra ₡{totalCarrito} </button>
            </div>
        {:else}
        <div class="cont">
            <h2 class="txt-subtitulo">No hay productos en el carrito</h2>
        </div>
        {/if}
        <ModalProductoSeleccionado productoSeleccionado={productoSeleccionado}/>
    {:else}
    <h2 class="txt-subtitulo">Debes iniciar sesión para agregar productos al carrito</h2>
    {/if}
</div>

<NavegacionInferior carrito="iconoActivo"/>