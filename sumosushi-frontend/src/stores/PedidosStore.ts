import { writable, get } from "svelte/store";
import { PedidoAdminModel } from "../models/Pedido/PedidoAdminModel";
import { obtenerPedidosService } from "../services/PedidoService";

export let pedidos = writable<PedidoAdminModel[]>([]);
export let hayPedidos = writable<boolean>(false);

export async function cargarPedidos(token: string): Promise<void> {
    const pedidosDesdeAPI = await obtenerPedidosService(token);
    pedidos.set(pedidosDesdeAPI);
    hayPedidos.set(pedidosDesdeAPI.length > 0);
}

export function estaPedidosVacio(): boolean {
    return get(pedidos).length === 0;
}
