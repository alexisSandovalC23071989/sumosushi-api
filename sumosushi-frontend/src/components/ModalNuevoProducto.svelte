<script lang="ts">
  import { obtenerAccesToken } from '../services/AuthService';

  import { NuevoProductoModel } from '../models/producto/NuevoProductoModel';
  import type { EtiquetaModel } from '../models/menu/EtiquetaModel';
  
  import { etiquetas } from '../stores/EtiquetasStore';
  import { categorias } from '../stores/CategoriaStore';
  import { NuevaEtiquetaModel } from '../models/etiqueta/NuevaEtiquetaModel';
  import { presentaciones } from '../stores/PresentacionStore';
  import { NuevaPresentacionProductoModel } from '../models/presentacion/NuevaPresentacionProductoModel';
  import { onMount } from 'svelte';
  import { registrarProductoService } from '../services/ProductoService';

  export let cerrarModal: () => void; 

  let nuevoProducto: NuevoProductoModel = new NuevoProductoModel();
  
  let paginaActual: number = 0;
  let urlImagen: string = "";

  onMount(async () => {
    nuevoProducto.Presentaciones = $presentaciones.map((presentacion) => new NuevaPresentacionProductoModel(presentacion));
  });

  function seleccionarImagen(event: Event) {
    const file = (event.target as HTMLInputElement).files?.[0];
    if (file) {
      urlImagen = URL.createObjectURL(file);
      nuevoProducto.ImagenFile = file;
    }
  };

  function comprobarEtiqueta(model: EtiquetaModel): boolean {
    return nuevoProducto.Etiquetas.some((etiqueta) => etiqueta.IdEtiqueta === model.IdEtiqueta);
  }  

  function manejarCambioEtiqueta(event: Event, model: EtiquetaModel) {
    const checkbox = event.target as HTMLInputElement;
    if (checkbox.checked) {
      nuevoProducto.Etiquetas.push(new NuevaEtiquetaModel(model.IdEtiqueta));
    } else {
      nuevoProducto.Etiquetas = nuevoProducto.Etiquetas.filter((etiqueta) => etiqueta.IdEtiqueta !== model.IdEtiqueta);
    }
  }

  function accionCancelar(){
    paginaActual = 0;
    nuevoProducto = new NuevoProductoModel();
    nuevoProducto.Presentaciones = $presentaciones.map((presentacion) => new NuevaPresentacionProductoModel(presentacion));
    urlImagen = "";
    cerrarModal();
  };

  function accionAnteriorPagina(){
    paginaActual --;
  };

  function accionSiguientePagina() {
    if(paginaActual === 0){
      if (nuevoProducto.Nombre && nuevoProducto.Descripcion && nuevoProducto.IdCategoria && nuevoProducto.ImagenFile) {
        paginaActual ++;
      } else {
        alert("Por favor, completa todos los campos");
      }
    }else if (paginaActual === 1){
      if(nuevoProducto.Presentaciones.length === 0){
        nuevoProducto.Presentaciones = $presentaciones.map((presentacion) => new NuevaPresentacionProductoModel(presentacion));
      }
      paginaActual ++;
    }
  };  

  async function accionRegistrar(){
    const errores = nuevoProducto.validar();
    if (errores.length > 0) {
      alert(errores.join("\n"));
      return;
    }
    const token = await obtenerAccesToken();
    if (!token) return;
    if(await registrarProductoService(token, nuevoProducto)) accionCancelar();
  }
</script>

<article class="modal">
  <div class="cont">
    <h2 class="txt-subtitulo">Nuevo producto</h2>

    {#if paginaActual === 0}
    <h2 class="txt-subtitulo">Paso 1</h2>
    <hr>
      <div class="cont-fila">
        <label for="nombreProducto" class="txt-texto">Nombre</label>
      </div>
      <input id="nombreProducto" class="input-txt" type="text" placeholder="Nombre..." bind:value={nuevoProducto.Nombre} />

      <div class="cont-fila">
        <label for="descripcionProducto" class="txt-texto">Descripción</label>
      </div>
      <input id="descripcionProducto" class="input-txt" type="text" placeholder="Descripción..." bind:value={nuevoProducto.Descripcion} />

      {#if urlImagen}
        <img src={urlImagen} alt="Imagen seleccionada" class="img-form" />
      {/if}

      <div class="cont-fila">
        <label for="imagenProducto" class="txt-texto">Imagen</label>
      </div>
      <input id="imagenProducto" class="input-txt" type="file" on:change={seleccionarImagen} />

      <div class="cont-fila">
        <label for="canjeablePuntos" class="txt-texto">Canjeable por puntos</label>
        <input id="canjeablePuntos" class="input-checkbox" type="checkbox" bind:checked={nuevoProducto.CanjeablePuntos} />
      </div>

      <div class="cont-fila">
        <label for="idCategoria" class="txt-texto">Categoría</label>
      </div>
      <select id="idCategoria" class="input-txt" bind:value={nuevoProducto.IdCategoria}>
        <option value="0">Seleccionar categoría</option>
        {#each $categorias as categoria}
          <option value={categoria.IdCategoria}>{categoria.Descripcion}</option>
        {/each}
      </select>

      <div class="cont-fila">
        <button class="btn" on:click={accionSiguientePagina}>Siguiente</button>
        <button class="btn btn-oscuro" on:click={accionCancelar}>Cancelar</button>
      </div>

    {/if}

    {#if paginaActual === 1}
    <h2 class="txt-subtitulo">Paso 2</h2>
    <hr>
    <div class="cont-fila">
      <label class="txt-texto" for="etiquetas"><strong>Etiquetas:</strong></label>
    </div>
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
          <button on:click={() => console.log($etiquetas)}>Mostrar</button>
        {/each}

      
        <button class="btn" on:click={accionSiguientePagina}>Siguiente</button>
        <div class="cont-fila">
          <button class="btn btn-oscuro" on:click={accionCancelar}>Cancelar</button>
          <button class="btn btn-oscuro" on:click={accionAnteriorPagina}>Anterior</button>
        </div>
    {/if}

    {#if paginaActual === 2}
    <h2 class="txt-subtitulo">Paso 3</h2>
    <hr>
    <div class="cont-fila">
      <h3 class="txt-texto">Presentaciones:</h3>
    </div>
    {#each nuevoProducto.Presentaciones as presentacion}
    <div class="cont-interno-justificado">
      <label for={presentacion.Presentacion} class="txt-texto">{presentacion.Presentacion} ₡</label>
      <input id={presentacion.Presentacion} type="number" bind:value={presentacion.Precio} class="input-txt" required/>
    </div>
    {/each}

    <button class="btn" on:click={accionRegistrar}>Registrar</button>
    <div class="cont-fila">
      <button class="btn btn-oscuro" on:click={accionCancelar}>Cancelar</button>
      <button class="btn btn-oscuro" on:click={accionAnteriorPagina}>Anterior</button>      
    </div>
    <button class="btn btn-oscuro" on:click={() => console.log(nuevoProducto)}>Mostrar</button>
    {/if}

  </div>
</article>
