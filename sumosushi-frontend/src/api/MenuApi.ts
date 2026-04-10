import { API_BASE } from '../Config';

const RUTAS_API = {
    OBTENER_VERSION: `${API_BASE}/Menu/ObtenerVersionMenu`,
    OBTENER_MENU: `${API_BASE}/Menu/MenuCompleto`,    
};

export async function obtenerVersionApi(): Promise<Response> {
    return await fetch(RUTAS_API.OBTENER_VERSION, {
        method: 'GET',
        headers: { 
            'Content-Type': 'application/json'}
    });
};

export async function obtenerMenuApi(): Promise<Response> {
    return await fetch(RUTAS_API.OBTENER_MENU, {
        method: 'GET',
        headers: { 
            'Content-Type': 'application/json'}
    });
};
