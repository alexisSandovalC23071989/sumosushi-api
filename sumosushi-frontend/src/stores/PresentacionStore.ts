
import {writable, get} from "svelte/store";

import type { PresentacionModel } from "../models/presentacion/PresentacionModel";
import { obtenerPresentacionesService } from "../services/PresentacionService";

export let presentaciones = writable<PresentacionModel[]>([]);

export async function cargarPresentaciones(token: string): Promise<void> {
    if (estaPresentacionesVacio()) {
        presentaciones.set(await obtenerPresentacionesService(token));
    }
}

export function estaPresentacionesVacio(): boolean {
    return get(presentaciones).length === 0;
}
