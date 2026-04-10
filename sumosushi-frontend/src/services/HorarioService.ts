import { obtenerDiasApi } from "../api/HorarioApi";
import { DiaModel } from "../models/horario/DiaModel";
import { responseEstatusOk } from "./utils/ResponseStatus";
import { infoSesion } from "../stores/AuthStores";
import { get } from 'svelte/store';
import { obtenerHorariosApi } from "../api/HorarioApi";
import { HorarioModel } from "../models/horario/HorarioModel";
import { registrarHorarioApi } from "../api/HorarioApi";
import { HorarioFormModel } from "../models/horario/HorarioFormModel";

export async function obtenerDiasService(token: string): Promise<DiaModel[]> {
    try{
        
        const response = await obtenerDiasApi(token);
        if (!await responseEstatusOk(response)) {
            return [];
        }
    
        const json = await response.json();
        if (!Array.isArray(json.datos)) {
            return [];
        }
    
        return json.datos.map((item: any) => new DiaModel(item.idDia, item.dia));
    }catch (error: any) {
        alert("Error de conexión. Por favor verifica tu conexión e intenta de nuevo.");    
        return [];    
    }
    
}

export async function obtenerHorariosService(): Promise<HorarioModel[]> {
    try{
    const response = await obtenerHorariosApi();
    if (!await responseEstatusOk(response)) {
        return [];
    }

    const json = await response.json();
    if (!Array.isArray(json.datos)) {
        return [];
    }

    return json.datos.map((item: any) => new HorarioModel(item.idDia, item.dia, item.horaApertura, item.horaCierre));
    }catch (error: any) {
        alert("Error de conexión. Por favor verifica tu conexión e intenta de nuevo.");    
        return [];    
    }
}

export async function registrarHorarioService(horario: HorarioFormModel): Promise<void> {
    try{
    const response = await registrarHorarioApi(horario, get(infoSesion).Jwt);
    if (!await responseEstatusOk(response)) {
        return;
    }

    const json = await response.json();
    if (!json.exito){
        alert(json.mensaje);
        return;
    }
    
    if (!Array.isArray(json.datos)) {
        return;
    }
    return;
    }catch (error: any) {
        alert("Error de conexión. Por favor verifica tu conexión e intenta de nuevo.");    
        return;    
    }
}

