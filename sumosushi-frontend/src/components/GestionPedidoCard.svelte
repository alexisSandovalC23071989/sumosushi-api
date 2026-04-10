<script lang="ts">
    import { PedidoAdminModel } from "../models/Pedido/PedidoAdminModel";
    import { EstadoModel } from "../models/estado/EstadoModel";
    import { audioHabilitado } from "../stores/AudioStore";
    import { ActualizarEstadoPedidoModel } from "../models/Pedido/ActualizarEstadoPedidoModel";
    import { actualizarEstadoPedidoService } from "../services/PedidoService";
    import { obtenerAccesToken } from "../services/AuthService";
    import { cargarPedidos } from "../stores/PedidosStore";

    const estados = [
        new EstadoModel(1, "Pendiente"),
        new EstadoModel(2, "En Proceso"),
        new EstadoModel(3, "Listo para retirar"),
        new EstadoModel(4, "Enviado"),
        new EstadoModel(5, "Completado"),
        new EstadoModel(6, "Cancelado")
    ]; 

    export let pedido: PedidoAdminModel;

    let desplegarInformacion: boolean = true;

    async function aceptarPedido() {
        const pedidoActualizar = new ActualizarEstadoPedidoModel(pedido.IdPedido, estados[1].IdEstado);

        const token = await obtenerAccesToken();
        if (!token) return;
        if(await actualizarEstadoPedidoService(pedidoActualizar, token)){
            cargarPedidos(token);
        }
        audioHabilitado.detener();
        desplegarInformacion = false;
    }

    async function cancelarPedido() {
        if (confirm("¿Estas seguro de cancelar el pedido?")) {
            const pedidoActualizar = new ActualizarEstadoPedidoModel(pedido.IdPedido, estados[5].IdEstado); 
            const token = await obtenerAccesToken();
            if (!token) return;
            if(await actualizarEstadoPedidoService(pedidoActualizar, token)){
                cargarPedidos(token);
            }
            audioHabilitado.detener();
            desplegarInformacion = false;
        }
    }

    async function cambiarEstado() {    
        // ** Punto de mejora, no permitir modificar el estado de la instancia en memoria para no tener que cargar los pedidos para 
        // devolverle su estado original antes de el cambio

        const pedidoActualizar = new ActualizarEstadoPedidoModel(pedido.IdPedido, pedido.IdEstadoPedido);
        const token = await obtenerAccesToken();
        if (!token) return;

        if (pedido.IdEstadoPedido === estados[5].IdEstado){
            if (!confirm("Seguro que desea cancelar el pedido?")){
                cargarPedidos(token);
                return;
            }
        }

        if (await actualizarEstadoPedidoService(pedidoActualizar, token)) {
            cargarPedidos(token);
        }
        
        cargarPedidos(token);
        audioHabilitado.detener();
        desplegarInformacion = false;
    }
</script>

<article class="card">
    <div class="cont-interno-justificado">

        <div class="cont-fila">
            <h2 class="txt-subtitulo">{pedido.IdPedido}</h2>
            <p class="txt-texto"><strong>Hora:</strong> {pedido.Hora}</p>
            <button 
                aria-label="Mostrar opciones" 
                class="btn-icon"
                onclick={() => desplegarInformacion = !desplegarInformacion}>
                <i class={`fas ${desplegarInformacion ? 'fa-chevron-up' : 'fa-chevron-down'} icon`}></i>
            </button>
            </div>

            {#if desplegarInformacion}            
                <p class="txt-texto"><strong>Cliente:</strong> {pedido.Nombre} {pedido.Apellidos}</p>
                <p class="txt-texto"><strong>Telefonos:</strong> {pedido.Telefonos}</p>
                {#if pedido.Direccion !== ""}
                    <p class="txt-texto"><strong>Direccion:</strong> {pedido.Direccion}</p>
                {/if}
            {/if}
                
            <table>
                <thead>
                    <tr>
                        <th>Item</th>
                        <th>Present</th>
                        <th>Cant</th>
                        <th>Subtotal</th>
                    </tr>
                </thead>
                <tbody>
                    {#each pedido.promociones as promocion}
                        <tr>
                            <td>{promocion.NombrePromocion}</td>
                            <td>Promocion</td>
                            <td>{promocion.Cantidad}</td>                            
                            <td>{promocion.Precio * promocion.Cantidad}</td>
                        </tr>
                    {/each}
                    {#each pedido.productos as producto}
                        <tr>
                            <td>{producto.NombreProducto}</td>
                            <td>{producto.Presentacion}</td>
                            <td>{producto.Cantidad}</td>                            
                            <td>{producto.Precio * producto.Cantidad}</td>
                        </tr>
                    {/each}
                </tbody>
            </table>               
            
            {#if desplegarInformacion}
            <p class="txt-texto"><strong>Metodo de pago:</strong> {pedido.MetodoPago}</p>
                {#if pedido.CostoEnvio > 0}
                    <p class="txt-texto"><strong>Costo de envio:</strong> ₡{pedido.CostoEnvio}</p>  
                {/if}
                {#if pedido.PuntosCanjeados > 0}
                    <p class="txt-texto"><strong>Puntos canjeados:</strong> {pedido.PuntosCanjeados}</p> 
                    <p class="txt-texto"><strong>Total con puntos canjeados:</strong> ₡{pedido.TotalConPuntos}</p>               
                {/if}
                <p class="txt-texto"><strong>Total pagado:</strong> ₡{pedido.TotalPagado}</p>
            {/if}

            <hr>
            <p class="txt-texto"><strong>Tipo de pedido:</strong> {pedido.TipoPedido}</p>
            {#if pedido.Notacliente !== ""}
                <p class="txt-texto"><strong>Nota:</strong> {pedido.Notacliente}</p>            
            {/if}

            {#if pedido.IdEstadoPedido === estados[0].IdEstado}
                <button class="btn" onclick={aceptarPedido}>Aceptar</button>
                <button class="btn btn-cancelar" onclick={cancelarPedido}>Cancelar</button>
            {:else}
                <select class="input-txt" bind:value={pedido.IdEstadoPedido} onchange={cambiarEstado}>
                    {#each estados as estado}
                        {#if estado.IdEstado !== estados[0].IdEstado}
                            <option value={estado.IdEstado}>{estado.Estado}</option>
                        {/if}
                    {/each}
                </select>
            {/if}       
          
    </div>
</article>