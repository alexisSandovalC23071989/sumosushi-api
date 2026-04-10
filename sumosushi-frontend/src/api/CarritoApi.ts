import { API_BASE } from '../Config';
import type { CarritoModel } from '../models/carrito/CarritoModel';

const RUTAS_API = {
    AGREGAR_PRODUCTO: `${API_BASE}/Carrito/Agregar`,
    ELIMINAR_PRODUCTO: `${API_BASE}/Carrito/Eliminar`,
    OBTENER_CARRITO: `${API_BASE}/Carrito/Obtener`,
};

export async function obtenerCarrito(token: string) : Promise<Response> {
    return await fetch(`${RUTAS_API.OBTENER_CARRITO}`, {
        method: 'GET',
        headers: { 
            'Content-Type': 'application/json', 
            'Authorization': `Bearer ${token}` 
        },
    });
};

export async function agregarProducto(token: string, carrito: CarritoModel) : Promise<Response> {
    return await fetch(`${RUTAS_API.AGREGAR_PRODUCTO}`, {
        method: 'POST',
        headers: { 
            'Content-Type': 'application/json', 
            'Authorization': `Bearer ${token}` 
        },
        body: JSON.stringify(carrito),
    });
};
