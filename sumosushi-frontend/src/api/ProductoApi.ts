import { API_BASE } from '../Config';

import type { ActualizarProductoModel } from '../models/producto/ActualizarProductoModel';
import type { ProductoCompletoModel } from '../models/menu/ProductoCompletoModel';
import type { NuevoProductoModel } from '../models/producto/NuevoProductoModel';
// import type { NuevoProductoModel } from '../models/menu/NuevoProductoModel';

export async function obtenerProductosApi(token: string): Promise<Response> {
    return await fetch(`${API_BASE}/Producto/ObtenerProductosPresentacionesTotales`, {
        method: 'GET',
        headers: { 
            'Content-Type': 'application/json', 
            'Authorization': `Bearer ${token}` }
    });
};

export async function actualizarProductoApi(token: string, model: ActualizarProductoModel): Promise<Response> {
    
    return await fetch(`${API_BASE}/Producto/ActualizarProducto`, {
        method: 'PUT',
        headers: {
            'Authorization': `Bearer ${token}`
        },
        body: model.toFormData()
    });
}

export async function registrarProductoApi(token: string, model: NuevoProductoModel): Promise<Response> {
    return await fetch(`${API_BASE}/Producto/RegistrarProducto`, {
        method: 'POST',
        headers: {
            'Authorization': `Bearer ${token}`
        },
        body: model.toFormData()
    });
}


export default {
    obtenerProductosApi,
    actualizarProductoApi,
    registrarProductoApi
};