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

    let desplegarInformacion: boolean = false;

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

            <div class="cont-fila-justificado">
                <h3 class = "txt-subtitulo">Estado del pedido:</h3>
                <p class="txt-subtitulo">{pedido.EstadoPedido}</p>  
            </div>
            <div class="card-fila">
                <i class="fas fa-clock icon" class:iconoActivo={pedido.IdEstadoPedido >= estados[0].IdEstado}></i>
                <p class="txt-texto" class:iconoActivo={pedido.IdEstadoPedido >= estados[0].IdEstado}>---></p>
            
                <i class="fas fa-spinner fa-spin icon" class:iconoActivo={pedido.IdEstadoPedido >= estados[1].IdEstado}></i>
                <p class="txt-texto" class:iconoActivo={pedido.IdEstadoPedido >= estados[1].IdEstado}>---></p>
            
                <i class="fas fa-check icon" class:iconoActivo={pedido.IdEstadoPedido >= estados[2].IdEstado}></i>
                <p class="txt-texto" class:iconoActivo={pedido.IdEstadoPedido >= estados[2].IdEstado}>---></p>
            
                {#if pedido.TipoPedido === "Domicilio"}
                    <i class="fas fa-motorcycle icon" class:iconoActivo={pedido.IdEstadoPedido >= estados[3].IdEstado}></i>
                    <p class="txt-texto" class:iconoActivo={pedido.IdEstadoPedido >= estados[3].IdEstado}>---></p>
                {/if}
            
                <i class="fas fa-check-circle icon" class:iconoActivo={pedido.IdEstadoPedido >= estados[4].IdEstado}></i>
            </div>      
            
            {#if desplegarInformacion}
                <hr>
                <p class="txt-texto"><strong>Cliente:</strong> {pedido.Nombre} {pedido.Apellidos}</p>
                <p class="txt-texto"><strong>Telefonos:</strong> {pedido.Telefonos}</p>
                {#if pedido.Direccion?.trim()}
                    <p class="txt-texto"><strong>Dirección:</strong> {pedido.Direccion}</p>
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
            <hr>
            <p class="txt-texto"><strong>Metodo de pago:</strong> {pedido.MetodoPago}</p>
                {#if pedido.CostoEnvio > 0}
                    <p class="txt-texto"><strong>Costo de envio:</strong> ₡{pedido.CostoEnvio}</p>  
                {/if}
                {#if pedido.PuntosCanjeados > 0}
                    <p class="txt-texto"><strong>Puntos canjeados:</strong> {pedido.PuntosCanjeados}</p> 
                    <p class="txt-texto"><strong>Total con puntos canjeados:</strong> ₡{pedido.TotalConPuntos}</p>               
                {/if}
                <p class="txt-texto"><strong>Total pagado:</strong> ₡{pedido.TotalPagado}</p>
                <hr>
                <p class="txt-texto"><strong>Tipo de pedido:</strong> {pedido.TipoPedido}</p>
                {#if pedido.Notacliente?.trim()}
                    <p class="txt-texto"><strong>Nota:</strong> {pedido.Notacliente}</p>            
                {/if}
            {:else}
                <p class="txt-texto"><strong>Total pagado:</strong> ₡{pedido.TotalPagado}</p>
            {/if}

            
    </div>
</article>