<script lang="ts">
    import type { UsuarioFormModel } from '../models/usuario/UsuarioFormModel';
    import type { RolModel } from '../models/rol/RolModel';
    import UsuarioCard from '../components/UsuarioCard.svelte';

    export let usuarios: UsuarioFormModel[] = [];
    export let roles: RolModel[] = [];
    export let cambiarRol: (event: Event, usuario: UsuarioFormModel) => void;
    export let rehabilitarCuenta: (usuario: UsuarioFormModel) => void; 
</script>

{#each usuarios as usuario}
        <UsuarioCard 
            usuario={usuario} 
            roles={roles} 
            rehabilitar={rehabilitarCuenta} 
            cambiarRol={cambiarRol} 
        />
{/each}