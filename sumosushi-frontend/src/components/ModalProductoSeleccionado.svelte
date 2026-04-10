<script lang="ts">
  import type { ProductoMenuModel } from "../models/menu/ProductoMenuModel";
  import { cargarCarrito, carrito } from "../stores/CarritoStore";
  import { obtenerAccesToken } from "../services/AuthService";
  import { agregarProductoService } from "../services/CarritoService";
  import { CarritoModel } from "../models/carrito/CarritoModel";
  import { goToCarrito } from "../routes/Routes";

  export let productoSeleccionado: ProductoMenuModel | null = null;

  $: totalCarrito = productoSeleccionado
    ? productoSeleccionado.Presentaciones.reduce((total, presentacion) => {
        const item = $carrito.find(i => i.IdPresentacionProducto === presentacion.IdPresentacionProducto);
        const cantidad = item?.Cantidad ?? 0;
        return total + presentacion.Precio * cantidad;
      }, 0)
    : 0;

  function getCantidad(idPresentacionProducto: number): number {
    return $carrito.find(item => item.IdPresentacionProducto === idPresentacionProducto)?.Cantidad ?? 0;
  }

  async function aumentarCantidad(idPresentacionProducto: number): Promise<void> {
    console.log("Aumentar", idPresentacionProducto);

    const token = await obtenerAccesToken();
    if (!token) return;

    const itemExistente = $carrito.find(item => item.IdPresentacionProducto === idPresentacionProducto);

    if (itemExistente) {
      const copiaItem = { ...itemExistente, Cantidad: itemExistente.Cantidad + 1 };
      const exito = await agregarProductoService(token, copiaItem);
      if (exito) cargarCarrito(token);
    } else {
      const nuevoItem: CarritoModel = {
        IdPresentacionProducto: idPresentacionProducto,
        IdProductoCarritoUsuario: 0,
        IdPromocion: 0,
        Cantidad: 1
      };
      const exito = await agregarProductoService(token, nuevoItem);
      if (exito) cargarCarrito(token);
    }
  }

  async function disminuirCantidad(idPresentacionProducto: number): Promise<void> {   
    console.log("Disminuir", idPresentacionProducto);
    const itemExistente = $carrito.find(item => item.IdPresentacionProducto === idPresentacionProducto);

    if (itemExistente) {
      var token = await obtenerAccesToken();
      if (!token) return;
      const copiaItem = { ...itemExistente };
      copiaItem.Cantidad--;

      if (await agregarProductoService(token, copiaItem)) {
        cargarCarrito(token);
      }
    }
  }

  function cerrarModal() {
    productoSeleccionado = null;
  }
</script>

  
  {#if productoSeleccionado}
    <div class="modal">
      <div class="producto">
        <div class="producto__img-container">
          <img class="producto__img" src={productoSeleccionado.ImagenUrl} alt="Imagen de {productoSeleccionado.Nombre}" />
        </div>
  
        <div class="producto__info">
          <h3 class="txt-subtitulo">{productoSeleccionado.Nombre}</h3>
          <p class="txt-texto">{productoSeleccionado.Descripcion}</p>
  
          {#if productoSeleccionado.Etiquetas}
            <p class="txt-texto">{productoSeleccionado.Etiquetas}</p>
          {/if}
  
          <hr class="producto__separador" />
  
          <h3 class="txt-texto">Presentaciones:</h3>

          {#each productoSeleccionado.Presentaciones as presentacion}
            <div class="card">
              <div class="cont-fila-justificado">
                <div class="cont-fila-justificado">
                  <p class="txt-texto">{presentacion.DetallePresentacion}</p>
                  <p class="txt-texto">₡{presentacion.Precio}</p>
                </div>
                
                <div class="producto__cantidad">
                  <button on:click={() => disminuirCantidad(presentacion.IdPresentacionProducto)} aria-label="Restar cantidad">
                    <i class="txt-texto"><strong>-</strong></i>
                  </button>
  
                  <p class="txt-texto">{getCantidad(presentacion.IdPresentacionProducto)}</p>
  
                  <button on:click={() => aumentarCantidad(presentacion.IdPresentacionProducto)} aria-label="Sumar cantidad">
                    <i class="txt-texto"><strong>+</strong></i>
                  </button>
                </div>
              </div>
            </div>
          {/each}
  
          <button class="btn" aria-label="Agregar al carrito" on:click={goToCarrito}>Total: ₡{totalCarrito}</button>
          <button class="btn btn-cancelar" on:click={cerrarModal} aria-label="Cerrar">Cerrar</button>
        </div>
      </div>
    </div>
  {/if}
  


<style>
    /* .producto__icono-cantidad{
        font-size:15px;
    } */
    /* .producto__titulo{
        display: flex;
        justify-content: space-between;
    }

    .producto__estrella{
        color: var(--color-secundario);
    }

    .producto__form{
        width:100%;
    }

    .producto__etiquetas{
        display:flex;
        gap:10px;
    }
    
    .producto__botones {
        display: flex;
        flex-direction: column;
        align-items: center;
        gap: 10px;
        width:100%;
    } */

    .modal {
        display: flex;
        justify-content: center;
        align-items: center;
        position: fixed;
        top: 0;
        left: 0;
        width: 100vw;
        height: 100vh;
        background-color: rgba(0, 0, 0, 0.8);
        z-index: 1000;
        pointer-events: all;
    }

    .producto {
        background-color: var(--color-fondo-principal);
        border-radius: var(--border-radius);
        padding: 10px;
        width: 98%;
        gap:10px;
        max-width:500px;
        box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
        overflow-y: auto;
        display: flex;
        flex-direction: column;
        align-items: center;
    }

    .producto__img-container {    
        display:flex;
        justify-content: center;
        margin:auto;
        width: 100%;
        max-width: 500px;
        border-radius: var(--border-radius);
        overflow: hidden;
        position: relative;
    }

    .producto__img {
        width: 100%;
        height: 100%;
        object-fit: cover;
    }

    .producto__info{
        width: 95%;
        display: flex;
        flex-direction: column;
        margin:10px;
        gap:10px;
    }

    .producto__cantidad{
        /* position: absolute;  */
        /* bottom: 0; 
        right: 0; */
        display: flex;
        align-items: center;
        justify-content: center;
        gap: 10px;
        /* margin: 9px 25px; */
        /* padding: 10px; */
        border-radius: var(--border-radius);
        background-color: var(--color-fondo-principal);
        opacity: 0.8;
    }

    .producto__cantidad p{
        background-color: var(--color-fondo-secundario);
        padding: 10px 15px;
        border-radius: var(--border-radius);
    }

    .producto__cantidad button{    
        border: 2px solid var(--color-texto-principal);    
        padding: 6px 10px;
        border-radius: 50%;
    }
/* 
    .producto__notaCliente {
        width: 100%;
        padding: 10px 15px;
        background-color: var(--color-fondo-secundario);
        border: 1px solid var(--color-texto-principal); 
        border-radius: var(--border-radius);
        color: var(--color-texto-principal);
        resize: none; 
        font-size: var(--tamanio-texto);
        font-family: inherit;
        outline: none;
        transition: border 0.5s;
    }

    .producto__notaCliente:focus {
        border: 1px solid var(--color-principal); 
        box-shadow: 0 0 5px var(--color-primario-transparente);
    } */

    .producto__separador {
        width: 100%; 
        border: 1px solid var(--color-texto-principal); 
        opacity: 0.3;
    }

</style>






