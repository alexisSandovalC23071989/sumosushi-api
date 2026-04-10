<script lang="ts">
  import { onMount } from 'svelte';

  // Stores
  import { get, writable } from 'svelte/store';
  import { perfil, cargarPerfil, estaPerfilVacio } from '../stores/PerfilUsuarioStore';

  // Componentes
  import Encabezado from '../components/Encabezado.svelte';
  import NavegacionInferior from '../components/NavegacionInferior.svelte';
  import ModalNuevoTelefono from '../components/ModalNuevoTelefono.svelte';
  import ModalNuevaDireccionUsuario from '../components/ModalNuevaDireccionUsuario.svelte';

  // Modelos
  import { PerfilCompletoUsuarioModel } from '../models/usuario/PerfilCompletoModel';
  
  // Servicios
  import { obtenerAccesToken } from '../services/AuthService';
  import { 
    actualizarPerfilService,
    guardarTelefonoService,
    eliminarTelefonoService, 
    guardarDireccionService,
    eliminarDireccionService} 
    from '../services/UsuarioService';
  import type { NuevaDireccionModel } from '../models/direccion/NuevaDireccionModel';

  let copiaPerfil = writable<PerfilCompletoUsuarioModel>(new PerfilCompletoUsuarioModel());
  
  // EstadoVista
  let editandoPerfil = $state(false);
  let modalNuevoTelefono = $state(false);
  let modalNuevaDireccion = $state(false);

  onMount(async () => {
    if (estaPerfilVacio()) {
      const token = await obtenerAccesToken();
      if (!token) return;
      await cargarPerfil(token);
    }
  });

  function activarEdicion() {
    copiaPerfil.set(get(perfil));
    editandoPerfil = true;
  };
  function cancelarCambios() {
    editandoPerfil = false;
  };
  async function guardarCambiosPerfil() {
    const token = await obtenerAccesToken();
    if (!token) return;
    if (await actualizarPerfilService(token, $copiaPerfil)) {
      perfil.set($copiaPerfil);
      editandoPerfil = false;
    }
  }; 
  function activarModalNuevoTelefono() {
    modalNuevoTelefono = true;
  };
  function cerrarModalNuevoTelefono() {
    modalNuevoTelefono = false;
  };
  async function guardarTelefono(telefono: string){
    const token = await obtenerAccesToken();
    if (!token) return;
    if (await guardarTelefonoService(token, telefono)) {
      const perfilActual = get(perfil);
      const nuevoPerfil = {
        ...perfilActual,
        Telefonos: [...perfilActual.Telefonos, telefono]
      };
      perfil.set(nuevoPerfil);
      cerrarModalNuevoTelefono();
    }
  };
  async function eliminarTelefono(telefono: string){
    const token = await obtenerAccesToken();
    if (!token) return;
    if (await eliminarTelefonoService(token, telefono)) {
      const perfilActual = get(perfil);
      const nuevoPerfil = {
        ...perfilActual,
        Telefonos: perfilActual.Telefonos.filter(t => t !== telefono)
      };
      perfil.set(nuevoPerfil);
    }
  };
  function activarModalNuevaDireccion() {
    modalNuevaDireccion = true;
  };
  function cerrarModalNuevaDireccion() {
    modalNuevaDireccion = false;
  };
  // Punto de mejora, traer solo las direcciones y no todo el perfil, 
  // al completar el registro de la nueva direccion 
  async function guardarDireccion(direccion: NuevaDireccionModel){
    const token = await obtenerAccesToken();
    if (!token) return;
    if (await guardarDireccionService(token, direccion)) {      
      await cargarPerfil(token, true);
      cerrarModalNuevaDireccion();
    }
  };
  async function eliminarDireccion(idDireccion: number){
    const token = await obtenerAccesToken();
    if (!token) return;
    if (await eliminarDireccionService(token, idDireccion)) {
      await cargarPerfil(token, true);
    }
  };

</script>
  
  <Encabezado />
  
  <div class="cont-main">
    <h1 class="txt-titulo">Mi perfil</h1>
  
    <!-- Información del usuario -->
    <section class="cont ">
      <div class="cont-fila">        
        {#if !editandoPerfil}
          <h2 class="txt-subtitulo">Información del usuario</h2>
          <button aria-label="Editar perfil" class="btn-icon" onclick={activarEdicion}>
            <i class="fas fa-pen-to-square icon"></i>
          </button>        
        {:else}
          <h2 class="txt-subtitulo">Editando información del usuario</h2>
        {/if}
      </div>
  
      <div class="cont-interno-justificado">
        <p class="txt-texto"><strong>Correo:</strong> {$perfil.Correo}</p>

        {#if editandoPerfil}
          <label>
            <strong class="txt-texto">Nombre:</strong>
            <input class="input-txt" type="text" bind:value={$copiaPerfil.Nombre} />
          </label>
          <label>
            <strong class="txt-texto">Apellido:</strong>
            <input class="input-txt" type="text" bind:value={$copiaPerfil.Apellidos} />
          </label>  
          <div class="perfil__acciones">
            <button class="btn" onclick={guardarCambiosPerfil}>Guardar</button>
            <button class="btn btn-oscuro" onclick={cancelarCambios}>Cancelar</button>
          </div>
        {:else}
          <p class="txt-texto"><strong>Nombre:</strong> {$perfil.Nombre}</p>
          <p class="txt-texto"><strong>Apellido:</strong> {$perfil.Apellidos}</p>        
        {/if} 
        <hr />        
      </div>   

      <div class="cont-fila">       
        <h2 class="txt-subtitulo">Teléfonos</h2>
        <button aria-label="Agregar teléfono" class="btn-icon" onclick={activarModalNuevoTelefono}>
          <i class="fas fa-circle-plus icon"></i>
        </button>        
      </div>
      
      {#if $perfil.Telefonos.length > 0}
        {#each $perfil.Telefonos as telefono}
        <div class="cont-fila-justificado">
          <button aria-label="Eliminar telefono" class="btn-icon" onclick={() => eliminarTelefono(telefono)}>
            <i class="fas fa-trash icon"></i>
          </button>  
          <p class="txt-texto">{telefono}</p>                  
        </div>
        {/each}
      {:else}
        <p class="txt-texto">No hay teléfonos registrados.</p>
      {/if}
    </section>
  
    <!-- Direcciones del usuario -->
    <section class="cont">
      <div class="cont-fila">
        <h3 class="txt-subtitulo">Direcciones guardadas</h3>
        <button aria-label="Agregar dirección" class="btn-icon" onclick={activarModalNuevaDireccion}>
          <i class="fas fa-circle-plus icon"></i>
        </button>
      </div>
  
      {#each $perfil.Direcciones as direccion (direccion.IdDireccion)}
        <article class="card">
          <div class="cont-interno-justificado">
            <p class="txt-texto"><strong>Canton:</strong> {direccion.Canton}</p>
            <p class="txt-texto"><strong>Distrito:</strong> {direccion.Distrito}</p>
            <p class="txt-texto"><strong>Detalle:</strong> {direccion.Detalle}</p>
          </div>
          <div class="cont-fila">            
            <button class="btn" onclick={() => eliminarDireccion(direccion.IdDireccion)}>Eliminar</button>
            <!-- <button class="btn btn-cancelar" onclick={() => editarDireccion(direccion.IdDireccion)}>Editar</button> -->
          </div>
        </article>
      {/each}
    </section>
  </div>

  <!-- Modales -->
   {#if modalNuevoTelefono}
    <ModalNuevoTelefono cerrarModal={cerrarModalNuevoTelefono} guardarTelefono={guardarTelefono}/>
   {/if}
   {#if modalNuevaDireccion}
    <ModalNuevaDireccionUsuario cerrarModal={cerrarModalNuevaDireccion} guardarDireccion={guardarDireccion}/>
   {/if}
  
  <NavegacionInferior perfil="iconoActivo" />
<style>
    .perfil__acciones {
        margin-top: 10px;
        display: flex;
        gap: 15px;
    }
</style>
