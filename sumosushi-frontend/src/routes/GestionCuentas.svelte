<script lang="ts">
    import { onMount } from 'svelte';

    // Componentes
    import Encabezado from '../components/Encabezado.svelte';
    import FiltradoColaboradores from '../components/FiltradoColaboradores.svelte';
    import FiltradoPorCorreo from '../components/FiltradoPorCorreo.svelte';
    import NavegacionInferior from '../components/NavegacionInferior.svelte';
    import UsuariosDeshabilitados from '../components/UsuariosDeshabilitados.svelte';

    //Modelos
    import { ActualizarRolModel } from '../models/usuario/ActualizarRolModel';
    import type { RolModel } from '../models/rol/RolModel';
    import type { UsuarioFormModel } from '../models/usuario/UsuarioFormModel';


    //Servicios
    import { obtenerAccesToken } from '../services/AuthService';
    import { obtenerRolesService } from '../services/RolService';
    import {
        actualizarRolUsuarioService,
        obtenerColaboradoresService,
        obtenerUsuarioPorCorreoService,
        obtenerUsuariosDeshabilitadosService,
        adminDeshabilitarCuentaService,
        adminRehabilitarCuentaService
    } from '../services/UsuarioService';

    const FILTRO_COLABORADORES = "1";
    const FILTRO_CORREO = "2";
    const FILTRO_DESHABILITADOS = "3";
    
    let usuarios: UsuarioFormModel[] = [];
    let roles: RolModel[] = [];
    let filtro = FILTRO_COLABORADORES;

    

    async function cambiarRol(event: Event, usuario: UsuarioFormModel) {
        const select = event.target as HTMLSelectElement;
        const nuevoRol = parseInt(select.value);
        const rolAnterior = usuario.IdRol;

        const confirmado = confirm(`¿Seguro que quieres cambiar el rol del usuario ${usuario.Nombre} a "${roles.find(r => r.IdRol === nuevoRol)?.Rol}"?`);
        if (!confirmado) {            
            select.value = rolAnterior.toString();
            return;
        }

        const token = await obtenerAccesToken();
        if (!token) {
            select.value = rolAnterior.toString();
            return;
        }

        const model: ActualizarRolModel = new ActualizarRolModel(usuario.IdUsuario, nuevoRol);

        const exito = await actualizarRolUsuarioService(token, model);
        if (!exito) select.value = rolAnterior.toString();
    }
    async function deshabilitarCuenta(usuario: UsuarioFormModel) {        
        const confirmado = confirm(`¿Seguro que quieres deshabilitar la cuenta del usuario ${usuario.Nombre} ${usuario.Apellidos}?`);
        if (!confirmado) return;

        const token = await obtenerAccesToken();
        if (!token) return;

        if (await adminDeshabilitarCuentaService(token, usuario)){
            refrescar();
        }        
    }
    async function rehabilitarCuenta(usuario: UsuarioFormModel) {        
        const confirmado = confirm(`¿Seguro que quieres rehabilitar la cuenta del usuario ${usuario.Nombre} ${usuario.Apellidos}?`);
        if (!confirmado) return;

        const token = await obtenerAccesToken();
        if (!token) return;

        if (await adminRehabilitarCuentaService(token, usuario)){
            refrescar();
        }        
    }
    async function filtrarPorCorreo(correo: string){
        const token = await obtenerAccesToken();
        if (!token) return;
        usuarios = await obtenerUsuarioPorCorreoService(token, correo);
    }
    async function refrescar(){
        const token = await obtenerAccesToken();
        if (!token) return;
        switch (filtro) {
            case FILTRO_COLABORADORES:
                usuarios = await obtenerColaboradoresService(token);
                break;
            case FILTRO_CORREO:
                usuarios = [];
                break;
            case FILTRO_DESHABILITADOS:
                usuarios = await obtenerUsuariosDeshabilitadosService(token);
                break;
        }
    }
    onMount(async () => {
        const token = await obtenerAccesToken();
        if (!token) return;
        refrescar();
        roles = await obtenerRolesService(token);
    });   
</script>

<Encabezado estadoRestaurante="Cerrado" />
<section class="cont-main">    
    <div class="cont">
        <h1 class="txt-titulo">Gestión de cuentas</h1>
        <hr>
        <div class="cont-interno-justificado">
            <label for="filtro" class="txt-subtitulo">Filtrar por:</label>            
        </div>
        <select id="filtro" class="input-txt" bind:value={filtro} on:change={refrescar}>
            <option value={FILTRO_COLABORADORES}>Colaboradores</option>
            <option value={FILTRO_CORREO}>Buscar por correo</option>
            <option value={FILTRO_DESHABILITADOS}>Deshabilitados</option>
        </select>
        {#if filtro === FILTRO_COLABORADORES}
            <FiltradoColaboradores 
                usuarios={usuarios}
                roles={roles}
                cambiarRol={cambiarRol}
                deshabilitarCuenta={deshabilitarCuenta}
            />
        {/if}
        {#if filtro === FILTRO_CORREO}
            <FiltradoPorCorreo 
                usuarios={usuarios}
                roles={roles}
                cambiarRol={cambiarRol}
                deshabilitarCuenta={deshabilitarCuenta}
                filtrarPorCorreo={filtrarPorCorreo}
            />
        {/if}
        {#if filtro === FILTRO_DESHABILITADOS}
            <UsuariosDeshabilitados 
                usuarios={usuarios}
                roles={roles}
                cambiarRol={cambiarRol}
                rehabilitarCuenta={rehabilitarCuenta}
            />
        {/if}
    </div>
</section>

<NavegacionInferior />
