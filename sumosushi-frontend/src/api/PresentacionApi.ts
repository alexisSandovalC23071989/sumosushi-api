import { API_BASE } from '../Config';


export async function obtenerPresentacionesApi(token: string): Promise<Response> {
    return await fetch(`${API_BASE}/Presentacion/ObtenerPresentaciones`, {
        method: 'GET',
        headers: { 
            'Content-Type': 'application/json', 
            'Authorization': `Bearer ${token}` }
    });
};

export default {
    obtenerPresentacionesApi
};
