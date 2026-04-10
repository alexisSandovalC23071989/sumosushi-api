<script lang="ts">

    import { onMount } from 'svelte'; 
    
    import { cantones, distritos, cargarDirecciones, direccionesVacios } from '../stores/DireccionesStores';
    import { NuevaDireccionModel } from '../models/direccion/NuevaDireccionModel';
    import { obtenerAccesToken } from '../services/AuthService';
    
    const {cerrarModal, guardarDireccion} = $props<{
        cerrarModal: () => void; 
        guardarDireccion: (direccion: NuevaDireccionModel) => void;
      }>();
  
    // Formulario
    // let nuevaDireccion = $state<NuevaDireccionModel>(new NuevaDireccionModel());
    let nuevaDireccion = $state({
      IdCanton: 0,
      IdDistrito: 0,
      Detalle: ''
    }as NuevaDireccionModel);

    // Cargar distritos al seleccionar cantón
    async function seleccionarCanton(event: Event) {
      const idCanton = Number((event.target as HTMLSelectElement).value);
      nuevaDireccion.IdCanton = idCanton;
      nuevaDireccion.IdDistrito = 0; 
    }

    onMount(async () => {
      if (direccionesVacios()) {
        const token = await obtenerAccesToken();
        if (!token) return;
        await cargarDirecciones(token);
      }
    });
  </script>
  
  <article class="modal">
    <div class="cont">
      <h2 class="txt-subtitulo">Nueva dirección</h2>
  
      <div class="cont-fila">
        <label for="canton" class="txt-texto">Cantón</label>
      </div>
      <select
        id="canton"
        class="input-txt"
        onchange={seleccionarCanton}
        bind:value={nuevaDireccion.IdCanton}
      >
        <option value="0" disabled selected>Seleccione un cantón...</option>
        {#each $cantones as canton}
          <option value={canton.IdCanton}>{canton.Canton}</option>
        {/each}
      </select>
  
      <div class="cont-fila">
        <label for="distrito" class="txt-texto">Distrito</label>
      </div>
      <select
        id="distrito"
        class="input-txt"
        bind:value={nuevaDireccion.IdDistrito}
        disabled={$distritos.filter(d => d.IdCanton === nuevaDireccion.IdCanton).length === 0}
      >
        <option value="" disabled selected>Seleccione un distrito...</option>
        {#each $distritos.filter(d => d.IdCanton === nuevaDireccion.IdCanton) as distrito}
          <option value={distrito.IdDistrito}>{distrito.Distrito}</option>
        {/each}
      </select>
  
      <div class="cont-fila">
        <label for="detalle" class="txt-texto">Detalle</label>
      </div>
      <input
        id="detalle"
        class="input-txt"
        type="text"
        bind:value={nuevaDireccion.Detalle}
      />
  
      <div class="cont-fila">
        <button class="btn" onclick={() => guardarDireccion(nuevaDireccion)}>Guardar</button>
        <button class="btn btn-oscuro" onclick={cerrarModal}>Cancelar</button>
      </div>
    </div>
  </article>
  