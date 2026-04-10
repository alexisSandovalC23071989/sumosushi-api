import { API_BASE } from '../Config';

export async function obtenerRolesApi(token: string): Promise<Response> {
    return await fetch(`${API_BASE}/Rol/ObtenerRoles`, {
        method: 'GET',
        headers: {'Content-Type': 'application/json', 'Authorization': `Bearer ${token}`}
    });
}
