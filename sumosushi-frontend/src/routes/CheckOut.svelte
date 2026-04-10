<script lang="ts">
    import { carrito } from "../stores/CarritoStore";
    import { cargarMenu, estaMenuVacio, menu } from "../stores/MenuStore";
    import { cargarCarrito } from "../stores/CarritoStore";
    import { obtenerAccesToken } from "../services/AuthService";
    import { cargarPerfil, estaPerfilVacio } from "../stores/PerfilUsuarioStore";
    import { onMount } from "svelte";
    import { infoSesion } from "../stores/AuthStores";
    import { goToLogin } from "./Routes";
    import Encabezado from "../components/Encabezado.svelte";
    import NavegacionInferior from "../components/NavegacionInferior.svelte";
    import { NuevoPedidoModel } from "../models/Pedido/NuevoPedidoModel";
    import { perfil } from "../stores/PerfilUsuarioStore";
    import {nuevoPedidoService} from "../services/PedidoService";
    import { goToSeguimientoPedido } from "./Routes";
    import { suscribirseAPush } from "../utils/push";

    let infoPedido = new NuevoPedidoModel();
    let canjearPuntos = false;
    let errorPuntos = "";
    let realizandoPedido = false;

    const COSTO_ENVIO = 1000;

    let infoPuntos = {
        minimoCanjeable: 500,
        maximoCanjeable: 1500,
        disponibles: 800
    };
    const TIPO_PEDIDO = {
        DOMICILIO: 1,
        PARA_LLEVAR: 2
    };
    const METODO_PAGO = {
        EFECTIVO: 1,
        TARJETA: 2
    };   

    function validarPuntosCanjeados() {
        errorPuntos = "";
        const puntos = infoPedido.PuntosCanjeados ?? 0;

        if (puntos > infoPuntos.disponibles) {
            errorPuntos = "No puedes canjear más puntos de los disponibles.";
        } else if (puntos < infoPuntos.minimoCanjeable) {
            errorPuntos = `No puedes canjear menos puntos de ₡${infoPuntos.minimoCanjeable}.`;
        }
    }

    async function realizarPedido() {
        realizandoPedido = true;
        var validacion = infoPedido.validar();
        if (validacion.length > 0) {
            alert(validacion.join("\n"));
            realizandoPedido = false;
            return;
        }
        
        const token = await obtenerAccesToken();
        if (!token) {
            realizandoPedido = false;
            return;
        };

        if(await nuevoPedidoService(infoPedido, token)) {
            suscribirseAPush();
            goToSeguimientoPedido();
            console.log("Pedido realizado exitosamente");            
            return;
        }
        realizandoPedido = false;
    }

    

    $: totalCarrito = $carrito.reduce((total, item) => {
        const producto = $menu.Categorias
            .flatMap(c => c.Productos)
            .find(p => p.Presentaciones.some(pr => pr.IdPresentacionProducto === item.IdPresentacionProducto));

        const presentacion = producto?.Presentaciones.find(pr => pr.IdPresentacionProducto === item.IdPresentacionProducto);
        const subtotal = presentacion ? presentacion.Precio * item.Cantidad : 0;

        return total + subtotal;
    }, 0);

    $: productosCarrito = $menu.Categorias
        .flatMap(c => c.Productos)
        .map(producto => {
            const presentacionesEnCarrito = producto.Presentaciones.filter(p =>
                $carrito.some(item => item.IdPresentacionProducto === p.IdPresentacionProducto)
            );

            if (presentacionesEnCarrito.length > 0) {
                return {
                    Nombre: producto.Nombre,
                    Presentaciones: presentacionesEnCarrito
                };
            }
            return null;
        })
        .filter(p => p !== null);

    $: totalConEnvio = totalCarrito + 
        (infoPedido.IdTipoPedido === TIPO_PEDIDO.DOMICILIO ? COSTO_ENVIO : 0) - 
        (infoPedido.PuntosCanjeados === undefined ? 0 : infoPedido.PuntosCanjeados);
        
    $: {
        if (infoPedido.IdTipoPedido === TIPO_PEDIDO.DOMICILIO && infoPedido.IdMetodoPago === METODO_PAGO.TARJETA) {
            infoPedido.IdMetodoPago = 0; 
        }
    }

    onMount(async () => {
        if (!$infoSesion.estaAutenticado) {
            goToLogin();
            return;
        }
        if (estaMenuVacio()) {
            await cargarMenu();
        }
        if ($carrito.length === 0) {
            const token = await obtenerAccesToken();
            if (!token) return;
            await cargarCarrito(token);
        }
        if (estaPerfilVacio()) {
            const token = await obtenerAccesToken();
            if (!token) return;
            await cargarPerfil(token);
        }
    });
</script>

<Encabezado estadoRestaurante="Cerrado" />

<div class="cont-main">
    <h2 class="txt-titulo">Checkout</h2>

    <div class="cont">
        <!-- <div class="cont-interno-justificado"> -->
            <h2 class="txt-titulo">Resumen de la compra</h2>
            <hr>

            <table>
                <thead>
                    <tr>
                        <th>Producto</th>
                        <th>Presentación</th>
                        <th>Cantidad</th>
                        <th>Subtotal</th>
                    </tr>
                </thead>
                <tbody>
                    {#each productosCarrito as producto}
                        {#each producto.Presentaciones as presentacion}
                            {#if $carrito.find(item => item.IdPresentacionProducto === presentacion.IdPresentacionProducto)}
                                <tr>
                                    <td>{producto.Nombre}</td>
                                    <td>{presentacion.DetallePresentacion}</td>
                                    <td>{$carrito.find(item => item.IdPresentacionProducto === presentacion.IdPresentacionProducto)?.Cantidad}</td>
                                    <td>₡{presentacion.Precio * ($carrito.find(item => item.IdPresentacionProducto === presentacion.IdPresentacionProducto)?.Cantidad ?? 0)}</td>
                                </tr>
                            {/if}
                        {/each}
                    {/each}
                </tbody>
            </table>            

            <select class="input-txt" bind:value={infoPedido.IdTipoPedido} id="tipoPedido">
                <option value={0}>Seleccione el tipo de pedido...</option>
                <option value={TIPO_PEDIDO.DOMICILIO}>Domicilio</option>
                <option value={TIPO_PEDIDO.PARA_LLEVAR}>Para llevar</option>
            </select>

            <!-- Selección de dirección solo si es domicilio -->
            {#if infoPedido.IdTipoPedido === TIPO_PEDIDO.DOMICILIO}
            <select class="input-txt" bind:value={infoPedido.IdDireccion} id="direccion">
                <option value={0}>Seleccione una dirección...</option>
                {#each $perfil.Direcciones as direccion}
                    <option value={direccion.IdDireccion}>{direccion.Detalle}</option>
                {/each}
            </select>
            {/if}

            <!-- Método de pago (opciones varían según el tipo de pedido) -->
            <select class="input-txt" bind:value={infoPedido.IdMetodoPago} id="metodoPago" disabled={infoPedido.IdTipoPedido === 0}>
                <option value={0}>Seleccione el método de pago...</option>
                {#if infoPedido.IdTipoPedido === TIPO_PEDIDO.DOMICILIO}
                    <option value={METODO_PAGO.EFECTIVO}>Efectivo</option>
                {:else if infoPedido.IdTipoPedido === TIPO_PEDIDO.PARA_LLEVAR}
                    <option value={METODO_PAGO.EFECTIVO}>Efectivo</option>
                    <option value={METODO_PAGO.TARJETA}>Tarjeta</option>
                {/if}
            </select>

            <hr>
            <div class="cont-fila-justificado">
                <input type="checkbox" bind:checked={canjearPuntos} on:change={() => infoPedido.PuntosCanjeados = 0} id="canjearPuntos" class="input-checkbox"/>
                <label for="canjearPuntos" class="txt-texto">Canjear puntos</label>
            </div>

            {#if canjearPuntos}            
                <div class="cont-fila-justificado">
                    <p class="txt-texto">Minimo canjeable: ₡{infoPuntos.minimoCanjeable}</p>
                    <p class="txt-texto">Maximo canjeable: ₡{infoPuntos.maximoCanjeable}</p>
                    <p class="txt-texto">Puntos disponibles: ₡{infoPuntos.disponibles}</p>
                </div>
                
                <div class="cont-fila-justificado">
                    <label for="puntosCanjeados" class="txt-texto">Puntos canjeados</label>
                    <input
                        type="number"
                        bind:value={infoPedido.PuntosCanjeados}
                        id="puntosCanjeados"
                        class="input-txt"
                        on:blur={validarPuntosCanjeados}
                    />
                </div>
                {#if errorPuntos}
                    <p class="txt-texto txt-error" >{errorPuntos}</p>
                {/if}
            {/if}

            <!-- ================================================================== -->

            <article class="card">
                <div class="cont-interno-justificado">
                    <p class="txt-texto"><strong>Subtotal:</strong> ₡{totalCarrito}</p>
                    <p class="txt-texto"><strong>Envio:</strong> ₡{infoPedido.IdTipoPedido === TIPO_PEDIDO.DOMICILIO ? COSTO_ENVIO : 0}</p>
                    <p class="txt-texto"><strong>Puntos canjeados:</strong> -₡{infoPedido.PuntosCanjeados}</p>
                    <hr>
                    <h2 class="txt-subtitulo">Total: ₡{totalConEnvio}</h2>                    
                </div>
            </article>

            <div class="cont-fila">
                <label for="notaCliente" class="txt-texto">Nota</label>
            </div>
            <textarea class="input-txt" bind:value={infoPedido.NotaCliente} id="notaCliente"></textarea>

            {#if !realizandoPedido}
                <button class="btn" on:click={realizarPedido}>Realizar compra</button>
            {:else}
                <button class="btn btn-oscuro" disabled>Realizando pedido...</button>
            {/if}
        <!-- </div> -->
    </div>
</div>

<NavegacionInferior carrito="iconoActivo" />
