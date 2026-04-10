import { API_BASE } from '../Config';

export async function obtenerEtiquetasApi(token: string): Promise<Response> {
    return await fetch(`${API_BASE}/Etiqueta/ObtenerEtiquetas`, {
        method: 'GET',
        headers: { 
            'Content-Type': 'application/json', 
            'Authorization': `Bearer ${token}` }
    });
};
