<script lang="ts">
  import type { RolModel } from "../models/rol/RolModel";
  import type { UsuarioFormModel } from "../models/usuario/UsuarioFormModel";

    const {usuario, roles, deshabilitar, cambiarRol, rehabilitar} = $props<{
        usuario: UsuarioFormModel;
        roles: RolModel[];
        deshabilitar?: (usuario: UsuarioFormModel) => void;
        cambiarRol: (event: Event, usuario: UsuarioFormModel) => void;
        rehabilitar?: (usuario: UsuarioFormModel) => void;
    }>();

    function accionDeshabilitar() {        
        deshabilitar(usuario);
    }

    function accionRehabilitar() {
        rehabilitar(usuario);
    }
    
    function accionCambiarRol(event: Event) {
        cambiarRol(event, usuario);
    }
</script>

<article class="card">
    <div class="cont-interno-justificado">
        <p class="txt-texto"><strong>Nombre:</strong> {usuario.Nombre} {usuario.Apellidos}</p>
        <p class="txt-texto"><strong>Correo:</strong> {usuario.Correo}</p>
        <select name="select-rol" class="input-txt" onchange={accionCambiarRol}>
            {#each roles as rol}
                <option value={rol.IdRol} selected={rol.IdRol === usuario.IdRol}>{rol.Rol}</option>
            {/each}
        </select>

    </div>
    {#if deshabilitar}
    <button class="btn" onclick={accionDeshabilitar}>Deshabilitar</button>
    {/if}
    {#if rehabilitar}
    <button class="btn" onclick={accionRehabilitar}>Rehabilitar</button>
    {/if}
</article>


<style>
    .card {
        width: 100%;
        display: flex;
        flex-direction: column;
        align-items: center;
        background-color: var(--color-fondo-principal);
        gap: 10px;
        padding: 10px 0;
        border-radius: var(--border-radius);
        border: 1px solid var(--color-principal);
    }
    .btn{
        width: 80%;
    }
    @media (min-width: 768px) {
        .btn{
            width: 90%;
        }
    }

</style>
