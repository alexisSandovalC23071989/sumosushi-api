import {writable, get } from 'svelte/store';

import type { CantonModel } from '../models/direccion/CantonModel';
import type { DistritoModel } from '../models/direccion/DistritoModel';

import { 
    obtenerCantonesService, 
    obtenerDistritosService } 
    from '../services/DireccionService';

export const cantones = writable<CantonModel[]>([]);
export const distritos = writable<DistritoModel[]>([]);

export function direccionesVacios(): boolean {
    return get(cantones).length === 0 && get(distritos).length === 0;
}

export async function cargarDirecciones(token: string): Promise<void> {
    if (direccionesVacios()) {
        const cantonesObtenidos = await obtenerCantonesService(token);
        cantonesObtenidos.sort((a, b) => a.Canton.localeCompare(b.Canton));
        cantones.set(cantonesObtenidos);
        console.log(get(cantones));

        const distritosObtenidos = await obtenerDistritosService(token);
        distritosObtenidos.sort((a, b) => {
            if (a.IdCanton !== b.IdCanton) {
                return a.IdCanton - b.IdCanton;
            }
            return a.Distrito.localeCompare(b.Distrito);
        });
        distritos.set(distritosObtenidos);
        console.log(get(distritos));
    }
}


export function obtenerDistritosPorCanton(idCanton: number): DistritoModel[] {
    return get(distritos).filter(distrito => distrito.IdCanton === idCanton);
}

