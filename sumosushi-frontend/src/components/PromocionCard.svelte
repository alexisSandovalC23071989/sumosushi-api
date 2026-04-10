<script lang="ts">
    import type { PromocionModel } from "../models/promocion/PromocionModel";
    import { ActualizarPromocionModel } from "../models/promocion/ActualizarPromocionModel";
    import { obtenerAccesToken } from "../services/AuthService";
    import { editarPromocionService } from "../services/PromocionService";
    import { cargarPromociones } from "../stores/PromocionesStore";

    export let promocion: PromocionModel;
    

    let actualizacionPromocion: ActualizarPromocionModel = new ActualizarPromocionModel(promocion);
    let editandoPromocion: boolean = false;
    let imagenPreview: string | null = null;

    function manejarCambioArchivo(event: Event) {
        const input = event.target as HTMLInputElement;
        if (input.files && input.files.length > 0) {
            const archivo = input.files[0];
            actualizacionPromocion.ImagenFile = archivo;

            if (imagenPreview) {
                URL.revokeObjectURL(imagenPreview);
            }

            imagenPreview = URL.createObjectURL(archivo);
        }
    };

    async function guardarPromocion(event: Event) {
        event.preventDefault();
        const errores = actualizacionPromocion.validar();
        if (errores.length > 0) {
            alert(errores.join("\n"));
            return;
        }

        const token = await obtenerAccesToken();
        if (!token) return;        

        if (await editarPromocionService(token, actualizacionPromocion)){
            cargarPromociones(token);
            editandoPromocion = false;
            imagenPreview = null;
            
        }
    };

    function cancelarEdicion() {
        editandoPromocion = false;
        imagenPreview = null;
        actualizacionPromocion = new ActualizarPromocionModel(promocion); // reactivo porque se reasigna
    };
</script>

<article class="card">
    <div class="cont-interno-justificado">

              
            {#if !editandoPromocion}
                <div class="cont-fila">
                    <h2 class="txt-subtitulo">{promocion.Nombre}</h2>
                    <button aria-label="Editar perfil" class="btn-icon" onclick={() => editandoPromocion = true}>
                        <i class="fas fa-pen-to-square icon"></i>
                    </button> 
                </div>
        
                <img src={promocion.ImagenUrl} alt="Imagen" class="img-promocion"/>
        
                <p class="txt-texto"><strong>Descripción:</strong>  {promocion.Descripcion}</p>
                <p class="txt-texto"><strong>Fecha Inicio:</strong> {promocion.FechaInicio}</p>
                <p class="txt-texto"><strong>Fecha Final:</strong> {promocion.FechaFinal}</p>
                <p class="txt-texto"><strong>Precio:</strong> ₡{promocion.Precio}</p>
        
                <div class="cont-fila-justificado"> 
                    <p class="txt-texto"><strong>Canjeable por Puntos:</strong></p>
                    <input type="checkbox" bind:checked={actualizacionPromocion.CanjeablePuntos} disabled/>
                </div>
        
                <p class="txt-texto"><strong>Estado:</strong> {promocion.Estado}</p>

                {:else}
                <h2 class="txt-subtitulo">Editando: {promocion.Nombre}</h2>
                <form class="form" enctype="multipart/form-data" onsubmit={guardarPromocion}>
  
                    <label class="txt-texto" for="descripcion"><strong>Descripción:</strong></label>
                    <input
                      id="descripcion"
                      name="descripcion"
                      type="text"
                      bind:value={actualizacionPromocion.Descripcion}
                      class="input-txt"
                      required
                    />
                  
                    <label class="txt-texto" for="fechaInicio"><strong>Fecha Inicio:</strong></label>
                    <input
                      id="fechaInicio"
                      name="fechaInicio"
                      type="date"
                      bind:value={actualizacionPromocion.FechaInicio}
                      class="input-txt"
                      required
                    />
                  
                    <label class="txt-texto" for="fechaFinal"><strong>Fecha Final:</strong></label>
                    <input
                      id="fechaFinal"
                      name="fechaFinal"
                      type="date"
                      bind:value={actualizacionPromocion.FechaFinal}
                      class="input-txt"
                      required
                    />

                    <label class="txt-texto" for="precio"><strong>Precio:</strong></label>
                    <input
                      id="precio"
                      name="precio"
                      type="number"
                      bind:value={actualizacionPromocion.Precio}
                      class="input-txt"
                      required
                    />
                  
                    <label class="txt-texto" for="imagenUrl"><strong>Imagen:</strong></label>
                    {#if imagenPreview}
                        <img src={imagenPreview} alt="Previsualización imagen" class="img-promocion"/>
                    {:else}
                        <img src={actualizacionPromocion.ImagenUrl} alt="Imagen actual" class="img-promocion"/>
                    {/if}
                  
                    <input
                      id="imagenUrl"
                      name="imagenUrl"
                      type="file"
                      accept="image/*"
                      onchange={manejarCambioArchivo}
                      class="input-txt"
                    />
                  
                    <div class="cont-fila-justificado">
                      <p class="txt-texto"><strong>Canjeable por Puntos:</strong></p>
                      <input
                        id="canjeablePuntos"
                        name="canjeablePuntos"
                        type="checkbox"
                        class="input-checkbox"
                        bind:checked={actualizacionPromocion.CanjeablePuntos}
                      />
                    </div>
                  
                    <label class="txt-texto" for="estado"><strong>Estado:</strong></label>
                    <select
                      id="estado"
                      name="estado"
                      bind:value={actualizacionPromocion.Estado}
                      class="input-txt"
                      required
                    >
                      <option value="Activo">Activo</option>
                      <option value="Inactivo">Inactivo</option>
                      <option value="Agotado">Agotado</option>
                    </select>
                  
                    <button class="btn" type="submit">Guardar</button>
                    <button type="button" class="btn btn-cancelar" onclick={cancelarEdicion}>Cancelar</button>
                  </form>
                  
              {/if}             
        
    </div>
</article>

<style>
    .img-promocion{
        width: 200px;
        height: 150px;
        object-fit: contain;
        background-color: var(--color-texto-secundario);
    }
</style>