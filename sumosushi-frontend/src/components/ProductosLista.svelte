<script lang="ts">
    import type { ProductoMenuModel } from '../models/menu/ProductoMenuModel';
    import { carrito } from '../stores/CarritoStore';
  
    export let categorias: { Categoria: string; Productos: ProductoMenuModel[] }[];
    export let mostrarDetalle: (producto: ProductoMenuModel) => void;
    export let modoCarrito: boolean = false;
  </script>
  
  {#each categorias as categoria}
    <div class="lista-productos" id={categoria.Categoria}>
        {#if !modoCarrito}
            <h2 class="txt-subtitulo" style="text-align: center;">{categoria.Categoria}</h2>
        {/if}
  
      {#each categoria.Productos as producto}
        {#if producto.Presentaciones.length > 0}
          <button class="producto" on:click={() => mostrarDetalle(producto)}>
            <div class="producto__img-container">
              <img class="producto__img" src={producto.ImagenUrl} alt="Imagen de {producto.Nombre}" />
            </div>
  
            <div class="producto__info">
              <h3 class="txt-subtitulo">{producto.Nombre}</h3>
              <p class="txt-texto">{producto.Descripcion}</p>
  
              {#each producto.Presentaciones as presentacion}
                {#if modoCarrito
                  && $carrito.some(item => item.IdPresentacionProducto === presentacion.IdPresentacionProducto)}
                  <p class="txt-texto">
                    {presentacion.DetallePresentacion} x {
                      $carrito.find(item => item.IdPresentacionProducto === presentacion.IdPresentacionProducto)?.Cantidad
                    } – ₡{presentacion.Precio}
                  </p>
                {:else if !modoCarrito && presentacion.Precio > 0}
                  <p class="txt-texto">{presentacion.DetallePresentacion} : ₡{presentacion.Precio}</p>
                {/if}
              {/each}
  
              {#if modoCarrito}
                <div class="producto__cantidad">
                  <p class="txt-texto">
                    {
                      producto.Presentaciones.reduce((acc, p) => {
                        const item = $carrito.find(i => i.IdPresentacionProducto === p.IdPresentacionProducto);
                        return acc + (item?.Cantidad ?? 0);
                      }, 0)
                    }
                  </p>
                </div>
              {/if}
            </div>
          </button>
        {/if}
      {/each}
    </div>
  {/each}
  

  
<style>
    .producto__img-container {
        width: 30%;
        border-radius: 12px;
        background-color: black;
        overflow: hidden;
        flex-shrink: 0; 
    }

    .producto__img {
        width: 100%;
        height: auto;
        object-fit: cover; 
        display: block;
    }

    .producto__cantidad {
        display: flex;
        position: relative;
        gap: 8px;
        position: absolute;
        top: 5px;
        right: 0;
    }
    
    .producto__cantidad p {
        background-color: var(--color-fondo-principal);
        padding: 5px 8px;
        border-radius: var(--border-radius);
        text-align: center;
    }

    .lista-productos {
        display: flex;
        flex-wrap: wrap;
        flex-direction: column;
        width: 95%;
        max-width: 800px;
        justify-content: center;
        gap: 8px;
        margin: 10px;
        scroll-margin-top: 145px;
    }

    .producto {
        display: flex;
        width: 100%;
        gap: 5px;
        padding: 5px;
        background-color: var(--color-fondo-secundario);
        border-radius: var(--border-radius);        
        display: flex;
        gap: 1rem;
        align-items: flex-start;
    }

    .producto__info {
        display: flex;
        flex-direction: column;
        text-align: left;
        gap: 5px;
        width: 100%;
        padding-bottom: 10px;
        position: relative;
    }

    .producto__cantidad {
        display: flex;
        position: relative;
        gap: 8px;
        position: absolute;
        top: 5px;
        right: 0;
    }
</style>