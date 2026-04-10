import { API_BASE } from '../Config';

import { InfoRestauranteModel } from '../models/restaurante/InfoRestauranteModel';

export async function obtenerInfoRestauranteApi(): Promise<Response> {
    return await fetch(`${API_BASE}/Rest/ObtenerInfoRestaurante`, {
        method: 'GET',
        headers: {'Content-Type': 'application/json'}
    });
}

export async function actualizarInfoRestauranteApi(infoRestaurante: InfoRestauranteModel, token: string): Promise<Response> {
    return await fetch(`${API_BASE}/Rest/ActualizarInfoRestaurante`, {
        method: 'PUT',
        headers: {'Content-Type': 'application/json', 'Authorization': `Bearer ${token}`},
        body: JSON.stringify(infoRestaurante)
    });
}
