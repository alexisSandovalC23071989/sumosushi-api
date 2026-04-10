<script lang="ts">
  import { obtenerAccesToken } from '../services/AuthService';
  import { registrarPromocionService } from '../services/PromocionService';

  import { NuevaPromocionModel } from '../models/promocion/NuevaPromocionModel';

  export let cerrarModal: () => void; 

  let nuevaPromocion: NuevaPromocionModel = new NuevaPromocionModel();

  let siguientePagina: boolean = false;
  let urlImagen: string = "";

  function seleccionarImagen(event: Event) {
    const file = (event.target as HTMLInputElement).files?.[0];
    if (file) {
      urlImagen = URL.createObjectURL(file);
      nuevaPromocion.ImagenFile = file;
    }
  };
  function accionSiguientePagina(){
    if(nuevaPromocion.Nombre && nuevaPromocion.Descripcion && nuevaPromocion.FechaInicio && nuevaPromocion.FechaFinal){
      siguientePagina = true;
    }else{
      alert("Por favor, completa todos los campos");
    }
  };
  function accionAnteriorPagina(){
    siguientePagina = false;
  };
  function accionCancelar(){
    siguientePagina = false;
    nuevaPromocion = new NuevaPromocionModel();
    urlImagen = "";
    cerrarModal();
  }; 
  async function guardarPromocion() {
    const token = await obtenerAccesToken();
    if (!token) return;

    const exito = await registrarPromocionService(token, nuevaPromocion);
    if (exito) {
      cerrarModal();
    }
  };
</script>

<article class="modal">
  <div class="cont">
    <h2 class="txt-subtitulo">Nueva promoción</h2>

    {#if siguientePagina === false}
      <div class="cont-fila">
        <label for="nombre" class="txt-texto">Nombre</label>
      </div>
      <input id="nombre" class="input-txt" type="text" placeholder="Nombre..." bind:value={nuevaPromocion.Nombre} />

      <div class="cont-fila">
        <label for="descripcion" class="txt-texto">Descripción</label>
      </div>
      <input id="descripcion" class="input-txt" type="text" placeholder="Descripción..." bind:value={nuevaPromocion.Descripcion} />

      <div class="cont-fila">
        <label for="fechaInicio" class="txt-texto">Fecha de inicio</label>
      </div>
      <input id="fechaInicio" class="input-txt" type="date" bind:value={nuevaPromocion.FechaInicio} />

      <div class="cont-fila">
        <label for="fechaFinal" class="txt-texto">Fecha de finalización</label>
      </div>
      <input id="fechaFinal" class="input-txt" type="date" bind:value={nuevaPromocion.FechaFinal} />

      <div class="cont-fila">
        <button class="btn" on:click={accionSiguientePagina}>Siguiente</button>
        <button class="btn btn-oscuro" on:click={accionCancelar}>Cancelar</button>
      </div>

    {:else}
      <div class="cont-fila">
        <label for="precio" class="txt-texto">Precio</label>
      </div>
      <input id="precio" class="input-txt" type="number" bind:value={nuevaPromocion.Precio} />

      <div class="cont-fila">
        <label for="canjeablePuntos" class="txt-texto">Canjeable por puntos</label>
        <input id="canjeablePuntos" class="input-checkbox" type="checkbox" bind:checked={nuevaPromocion.CanjeablePuntos} />
      </div>

      {#if urlImagen}
        <img src={urlImagen} alt="Imagen seleccionada" class="img-form" />
      {/if}

      <div class="cont-fila">
        <label for="imagen" class="txt-texto">Imagen</label>
      </div>
      <input id="imagen" class="input-txt" type="file" on:change={seleccionarImagen} />

      <div class="cont-fila">
        <button class="btn btn-oscuro" on:click={cerrarModal}>Cancelar</button>
        <button class="btn btn-oscuro" on:click={accionAnteriorPagina}>Anterior</button>        
      </div>
      <button class="btn" on:click={guardarPromocion}>Guardar</button>
      
    {/if}
  </div>
</article>
