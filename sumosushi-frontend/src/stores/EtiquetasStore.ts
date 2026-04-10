
import { writable, get } from "svelte/store";
import type { EtiquetaModel } from "../models/menu/EtiquetaModel";
import { obtenerEtiquetasService } from "../services/EtiquetaService";

export let etiquetas = writable<EtiquetaModel[]>([]);

export async function cargarEtiquetas(token: string): Promise<void> {
    if (estaEtiquetasVacio()) {
        etiquetas.set(await obtenerEtiquetasService(token));
    }
}

export function estaEtiquetasVacio(): boolean {
    return get(etiquetas).length === 0;
}