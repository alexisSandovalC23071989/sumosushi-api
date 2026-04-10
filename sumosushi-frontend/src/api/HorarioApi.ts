import { API_BASE } from '../Config';
import type { HorarioFormModel } from '../models/horario/HorarioFormModel';


export async function obtenerDiasApi(token: string): Promise<Response> {
    return await fetch(`${API_BASE}/Horario/ObtenerDias`, {
        method: 'GET',
        headers: { 
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}`}
    });
}

export async function obtenerHorariosApi(): Promise<Response> {
    return await fetch(`${API_BASE}/Horario/ObtenerHorariosRestaurante`, {
        method: 'GET',
        headers: {'Content-Type': 'application/json'}
    });
}

export async function registrarHorarioApi(horario: HorarioFormModel, token: string): Promise<Response> {
    return await fetch(`${API_BASE}/Horario/RegistrarHorario`, {
        method: 'POST',
        headers: {'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}`
        },
        body: JSON.stringify(horario)
    });
}


