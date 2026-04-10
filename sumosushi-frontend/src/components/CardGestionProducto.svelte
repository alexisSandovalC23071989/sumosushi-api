<script lang="ts">
  import type { ProductoCompletoModel } from "../models/menu/ProductoCompletoModel";
  import { ActualizarProductoModel } from "../models/producto/ActualizarProductoModel";
  import { EtiquetaModel } from "../models/menu/EtiquetaModel";
  import { EtiquetaProductoModel } from "../models/menu/EtiquetaProductoModel";

  import { obtenerAccesToken } from "../services/AuthService";  
  import { editarProductoService } from "../services/ProductoService";

  import { cargarProductos } from "../stores/ProductosStore";
  import { categorias } from "../stores/CategoriaStore";
  import { etiquetas } from "../stores/EtiquetasStore";
  // import { presentaciones } from "../stores/PresentacionStore";

  export let producto: ProductoCompletoModel;

  let actualizacionProducto: ActualizarProductoModel = new ActualizarProductoModel(producto);
  let editandoProducto: number = 0;
  let imagenPreview: string | null = null;
  let desplegarInformacion: boolean = false;


  function manejarCambioArchivo(event: Event) {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length > 0) {
      const archivo = input.files[0];
      actualizacionProducto.ImagenFile = archivo;

      if (imagenPreview) {
        URL.revokeObjectURL(imagenPreview);
      }

      imagenPreview = URL.createObjectURL(archivo);
    }
  }
  async function actualizarProducto(event: Event) {
    event.preventDefault();
    const errores = actualizacionProducto.validar();
    if (errores.length > 0) {
      alert(errores.join("\n"));
      return;
    }
    
    const token = await obtenerAccesToken();
    if (!token) return;

    if (await editarProductoService(token, actualizacionProducto)) {
      cargarProductos(token);
      editandoProducto = 0;
      imagenPreview = null;
    }
  }
  function cancelarEdicion() {
    editandoProducto = 0;
    imagenPreview = null;
    actualizacionProducto = new ActualizarProductoModel(producto);
  }
  function iniciarEdicion() {
    console.log(actualizacionProducto);
    editandoProducto = 1;
    actualizacionProducto = new ActualizarProductoModel(producto);

  }
  //Para asignar valor al checkBox
  function comprobarEtiqueta(model: EtiquetaModel): boolean {
    return actualizacionProducto.Etiquetas.some((etiqueta) => etiqueta.IdEtiqueta === model.IdEtiqueta);
  }  
  function manejarCambioEtiqueta(event: Event, model: EtiquetaModel) {
    const input = event.target as HTMLInputElement;
    const checked = input.checked;
    const nuevaEtiqueta = new EtiquetaProductoModel(producto.IdProducto, model.IdEtiqueta, model.Etiqueta);
    
    if (checked) {
      actualizacionProducto.Etiquetas.push(nuevaEtiqueta);
    } else {
      actualizacionProducto.Etiquetas = actualizacionProducto.Etiquetas.filter((etiqueta) => etiqueta.IdEtiqueta !== nuevaEtiqueta.IdEtiqueta);
    }
  }


</script>

<article class="card">
  <div class="cont-interno-justificado">
    <div class="cont-fila">
      <h2 class="txt-subtitulo">{producto.Nombre}</h2>
      <button 
        aria-label="Mostrar opciones" 
        class="btn-icon"
        on:click={() => desplegarInformacion = !desplegarInformacion}>
        <i class={`fas ${desplegarInformacion ? 'fa-chevron-up' : 'fa-chevron-down'} icon`}></i>
      </button>
    </div>

    {#if desplegarInformacion}
    <hr>
      {#if editandoProducto === 0}
        <img src={producto.ImagenUrl} alt="Imagen del producto" class="img-promocion" />

        <p class="txt-texto"><strong>Descripción:</strong> {producto.Descripcion}</p>
        <p class="txt-texto"><strong>Categoría:</strong> {producto.Categoria}</p>

        <div class="cont-fila-justificado">
          <p class="txt-texto"><strong>Canjeable por Puntos:</strong> {producto.CanjeablePuntos ? 'Sí' : 'No'}</p>
        </div>

        <p class="txt-texto"><strong>Estado:</strong> {producto.Estado}</p>

        {#if producto.Etiquetas.length > 0}
          <hr />
          <p class="txt-texto"><strong>Etiquetas:</strong></p>
          {#each producto.Etiquetas as etiqueta}
            <p class="txt-texto">{etiqueta.DetalleEtiqueta}</p>
          {/each}
        {/if}

        {#if producto.Presentaciones.length > 0}
          <hr />
          <p class="txt-texto"><strong>Presentaciones:</strong></p>
          {#each producto.Presentaciones as presentacion}
            <p class="txt-texto">{presentacion.DetallePresentacion}: ₡{presentacion.Precio}</p>
          {/each}
        {/if}

        <button aria-label="Editar producto" class="btn" on:click={iniciarEdicion}>
          Editar
        </button>

      {:else if editandoProducto === 1}
        <h2 class="txt-subtitulo">Editando paso 1:</h2>

        <label class="txt-texto" for="descripcion"><strong>Descripción:</strong></label>
        <input
          id="descripcion"
          name="descripcion"
          type="text"
          bind:value={actualizacionProducto.Descripcion}
          class="input-txt"
          required
        />

        <label class="txt-texto" for="categoria"><strong>Categoría:</strong></label>
        <select
          id="categoria"
          name="categoria"
          bind:value={actualizacionProducto.IdCategoria}
          class="input-txt"
          required
        >
          {#each $categorias as categoria}
            <option value={categoria.IdCategoria}>{categoria.Descripcion}</option>
          {/each}
        </select>

        <label class="txt-texto" for="imagenUrl"><strong>Imagen:</strong></label>
        {#if imagenPreview}
          <img src={imagenPreview} alt="Previsualización imagen" class="img-promocion" />
        {:else}
          <img src={actualizacionProducto.ImagenUrl} alt="Imagen actual" class="img-promocion" />
        {/if}

        <input
          id="imagenUrl"
          name="imagenUrl"
          type="file"
          accept="image/*"
          on:change={manejarCambioArchivo}
          class="input-txt"
        />

        <div class="cont-fila-justificado">
          <p class="txt-texto"><strong>Canjeable por Puntos:</strong></p>
          <input
            id="canjeablePuntos"
            name="canjeablePuntos"
            type="checkbox"
            class="input-checkbox"
            bind:checked={actualizacionProducto.CanjeablePuntos}
          />
        </div>

        <label class="txt-texto" for="estado"><strong>Estado:</strong></label>
        <select
          id="estado"
          name="estado"
          bind:value={actualizacionProducto.Estado}
          class="input-txt"
          required
        >
          <option value="Activo">Activo</option>
          <option value="Inactivo">Inactivo</option>
          <option value="Agotado">Agotado</option>
        </select>

        <button type="button" class="btn btn-cancelar" on:click={cancelarEdicion}>Cancelar</button>
        <button type="button" class="btn" on:click={() => editandoProducto = 2}>Siguiente</button>

      {:else if editandoProducto === 2}
        <h2 class="txt-subtitulo">Editando paso 2:</h2>

        <label class="txt-texto" for="etiquetas"><strong>Etiquetas:</strong></label>
        {#each $etiquetas as etiqueta}
          <div class="cont-fila-justificado">
            <input
              type="checkbox"
              checked={comprobarEtiqueta(etiqueta)}
              class="input-checkbox"
              on:change={(e) => manejarCambioEtiqueta(e, etiqueta)}
            />
            <label class="txt-texto" for="etiquetas">{etiqueta.Etiqueta}</label>
          </div>
        {/each}

        <button type="button" class="btn" on:click={() => editandoProducto = 3}>Siguiente</button>
        <button type="button" class="btn btn-cancelar" on:click={cancelarEdicion}>Cancelar</button>

      {:else if editandoProducto === 3}
        <h2 class="txt-subtitulo">Editando paso 3:</h2>

        <h3 class="txt-texto">Presentaciones:</h3>

        {#each actualizacionProducto.Presentaciones as presentacion}
          <label class="txt-texto" for={presentacion.DetallePresentacion}>{presentacion.DetallePresentacion}</label>
          <input type="number" bind:value={presentacion.Precio} class="input-txt" id={presentacion.DetallePresentacion}/>          
        {/each}

        <button type="button" class="btn" on:click={actualizarProducto}>Guardar</button>
        <button type="button" class="btn btn-cancelar" on:click={cancelarEdicion}>Cancelar</button>
      {/if}
    {/if}
  </div>
</article>


<style>
  .img-promocion {
    width: 200px;
    height: 150px;
    object-fit: contain;
    background-color: var(--color-texto-secundario);
  }
</style>
