import { API_BASE } from "../Config";

export async function obtenerCantonesApi(token: string): Promise<Response> {
    return await fetch(`${API_BASE}/Direccion/GetCantones`, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}`
        }
    });
}

export async function obtenerDistritosApi(token: string): Promise<Response> {
    return await fetch(`${API_BASE}/Direccion/GetDistritos`, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}`
        }
    });
}

export async function obtenerDireccionesUsuarioApi(token: string): Promise<Response> {
    return await fetch(`${API_BASE}/Direccion/GetDireccionesUsuario`, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}`
        }
    });
}
