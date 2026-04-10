import { writable, get } from "svelte/store";
import { PedidoAdminModel } from "../models/Pedido/PedidoAdminModel";
import { obtenerMisPedidosService } from "../services/PedidoService";
import { obtenerAccesToken } from "../services/AuthService";

export let misPedidos = writable<PedidoAdminModel[]>([]);
export let hayMisPedidos = writable<boolean>(false);

export async function cargarMisPedidos(token: string): Promise<void> {
    const misPedidosDesdeAPI = await obtenerMisPedidosService(token);
    misPedidos.set(misPedidosDesdeAPI);
    hayMisPedidos.set(misPedidosDesdeAPI.length > 0);
}

export function estaMisPedidosVacio(): boolean {
    return get(misPedidos).length === 0;
}

//
