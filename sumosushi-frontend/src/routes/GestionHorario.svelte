<script lang="ts">
    // COMPONENTES
    import Encabezado from '../components/Encabezado.svelte';
    import NavegacionInferior from '../components/NavegacionInferior.svelte';
    
    // MODELOS
    import {HorarioModel} from '../models/horario/HorarioModel';
    import {DiaModel} from '../models/horario/DiaModel';
    import { HorarioFormModel } from '../models/horario/HorarioFormModel';
    
    // SERVICIOS
    import {onMount} from 'svelte';
    import {obtenerDiasService, obtenerHorariosService, registrarHorarioService} from '../services/HorarioService';
    import { obtenerAccesToken } from '../services/AuthService';
    
    
    // ROUTAS
    import { goToLogin } from "../routes/Routes";

    let diasSemana: DiaModel[] = [];
    let horarios: HorarioModel[] = [];
    let horarioFormulario = new HorarioFormModel();

    onMount(async () => {
        const token = await obtenerAccesToken();
        if (!token) {
          goToLogin();
          return;
        }
        diasSemana = await obtenerDiasService(token);
        horarios = await obtenerHorariosService();
    });

    async function registrarHorario(event: Event){
        event.preventDefault();
        await registrarHorarioService(horarioFormulario.toDTO());
        horarios = await obtenerHorariosService();
        horarioFormulario = new HorarioFormModel();
    }
    

</script>


<section class="cont-main">    
    <article class="cont">
        <Encabezado estadoRestaurante="Cerrado"/>
        <h1 class="txt-titulo">Gestionar de horario</h1>
        <table>
            <thead>
              <tr>
                <th style="display: none;">ID</th>
                <th>Día</th>
                <th>Apertura</th>
                <th>Cierre</th>
                <th>Acciones</th>
              </tr>
            </thead>
            <tbody>
                {#each horarios as horario, i}
                    <tr>
                        <td style="display: none;">{horario.IdDia}</td> 
                        <td>{horario.Dia}</td> 
                        <td>{horario.HoraApertura}</td>
                        <td>{horario.HoraCierre}</td>
                        <td class="td-centrado">
                        <button class="btn-icon" aria-label="Eliminar horario"><i class="fa-solid fa-trash"></i></button>
                        </td>
                    </tr>
                {/each}
            </tbody>            
        </table>
    </article>   

    <article class="cont">
        
        <select id="selectDia" class="input-txt" bind:value={horarioFormulario.IdDia}>
            <option disabled selected value={null}>Seleccione un día...</option>
            {#each diasSemana as dia}
              <option value={dia.IdDia}>{dia.Dia}</option>
            {/each}
        </select>
        <label for="apertura" class="txt-subtitulo">Apertura</label>
            <input id="apertura" type="time" class="input-txt" bind:value={horarioFormulario.HoraApertura}>
        <label for="cierre" class="txt-subtitulo">Cierre</label>
            <input id="cierre" type="time" class="input-txt" bind:value={horarioFormulario.HoraCierre}>
        <button class="btn" onclick={registrarHorario}>Guardar</button>
    </article> 
</section>

<NavegacionInferior/>
