import { API_BASE } from '../Config';

//Modelos
import type { GestionCategoriaModel } from '../models/menu/GestionCategoriaModel';
import type { NuevaCategoriaModel } from '../models/menu/NuevaCategoriaModel';

const RUTAS_API = {
    OBTENER_CATEGORIAS: `${API_BASE}/Categoria/ObtenerCategorias`,
    DESHABILITAR_CATEGORIA: `${API_BASE}/Categoria/DeshabilitarCategoria`,
    HABILITAR_CATEGORIA: `${API_BASE}/Categoria/RehabilitarCategoria`,
    CREAR_CATEGORIA: `${API_BASE}/Categoria/AgregarCategoria`,
};

export async function obtenerCategoriasApi(token: string): Promise<Response> {
    return await fetch(`${RUTAS_API.OBTENER_CATEGORIAS}`, {
        method: 'GET',
        headers: { 'Authorization': `Bearer ${token}` },
    });
}

export async function deshabilitarCategoriaApi(token: string, categoria: GestionCategoriaModel): Promise<Response> {
    return await fetch(`${RUTAS_API.DESHABILITAR_CATEGORIA}`, {
        method: 'PUT',
        headers: { 
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}` },
        body: JSON.stringify({IdCategoria: categoria.IdCategoria}),
    });
}

export async function habilitarCategoriaApi(token: string, categoria: GestionCategoriaModel): Promise<Response> {
    return await fetch(`${RUTAS_API.HABILITAR_CATEGORIA}`, {
        method: 'PUT',
        headers: { 
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}` },
        body: JSON.stringify({IdCategoria: categoria.IdCategoria}),
    });
}

export async function crearCategoriaApi(token: string, categoria: NuevaCategoriaModel): Promise<Response> {
    return await fetch(`${RUTAS_API.CREAR_CATEGORIA}`, {
        method: 'POST',
        headers: { 
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}` },
        body: JSON.stringify(categoria),
    });
}