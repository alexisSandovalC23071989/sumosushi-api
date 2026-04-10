<script lang="ts">    

    const { cerrarModal, guardarTelefono } = $props<{ cerrarModal: () => void, guardarTelefono: (telefono: string) => void }>();

    let telefono = $state('');

    function cancelar() {
        cerrarModal();
    };    

</script>

<article class="modal">
    <div class="cont">
        <h2 class="txt-subtitulo">Nuevo teléfono</h2>
        <input id="telefono" class="input-txt" type="text" bind:value={telefono} />
        <div class="cont-fila">
            <button class="btn" onclick={() => guardarTelefono(telefono)}>Guardar</button>
            <button class="btn btn-oscuro" onclick={cancelar}>Cancelar</button>
        </div>
    </div>
</article>

<style>
    
</style>