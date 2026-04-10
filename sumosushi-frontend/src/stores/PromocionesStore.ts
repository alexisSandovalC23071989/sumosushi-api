import { writable, get } from 'svelte/store';
import type { PromocionModel } from '../models/promocion/PromocionModel';
import { obtenerPromocionesService } from "../services/PromocionService";

export let promociones = writable<PromocionModel[]>([]);

export async function cargarPromociones(token: string): Promise<void> {
    promociones.set(await obtenerPromocionesService(token));
}

export function estaPromocionesVacio(): boolean {
    return get(promociones).length === 0;
}
