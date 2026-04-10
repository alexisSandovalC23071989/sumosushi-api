import {writable, get} from 'svelte/store';
import type { GestionCategoriaModel } from '../models/menu/GestionCategoriaModel';
import { obtenerCategoriasService } from '../services/CategoriaService';

export let categorias = writable<GestionCategoriaModel[]>([]);

export function categoriasVacios(): boolean {
    return get(categorias).length === 0;
};

export async function cargarCategorias(token: string): Promise<void> {
    const categoriasObtenidas = await obtenerCategoriasService(token);
    
    categoriasObtenidas.sort((a, b) => {
        // Primero por EstadoBool (activos primero)
        const estadoA = Number(a.EstadoBool);
        const estadoB = Number(b.EstadoBool);
        if (estadoA !== estadoB) {
            return estadoB - estadoA;
        }
        // Luego por Descripcion
        return a.Descripcion.localeCompare(b.Descripcion);
    });

    categorias.set(categoriasObtenidas);
};
