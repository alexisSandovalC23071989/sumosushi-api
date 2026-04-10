<script lang="ts">
    import type { UsuarioFormModel } from '../models/usuario/UsuarioFormModel';
    import type { RolModel } from '../models/rol/RolModel';
    import UsuarioCard from '../components/UsuarioCard.svelte';

    export let usuarios: UsuarioFormModel[] = [];
    export let roles: RolModel[] = [];
    export let cambiarRol: (event: Event, usuario: UsuarioFormModel) => void;
    export let deshabilitarCuenta: (usuario: UsuarioFormModel) => void;  
    export let filtrarPorCorreo: (correo: string) => void;  

    let correo = '';
</script>

<div class="cont-fila">
    <input class="input-txt" type="text" placeholder="Buscar por correo" bind:value={correo}/>
    <button class="btn" onclick={() => filtrarPorCorreo(correo)}>Buscar</button>
</div>
{#each usuarios as usuario}
        <UsuarioCard 
            usuario={usuario} 
            roles={roles} 
            deshabilitar={deshabilitarCuenta} 
            cambiarRol={cambiarRol} 
        />
{/each}