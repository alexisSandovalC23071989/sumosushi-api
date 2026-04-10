
import {API_BASE} from '../Config';
import type { NuevoPedidoModel } from '../models/Pedido/NuevoPedidoModel';
import type { ActualizarEstadoPedidoModel } from '../models/Pedido/ActualizarEstadoPedidoModel';

export async function registrarPedidoApi(pedido: NuevoPedidoModel, token: string){
    return await fetch(`${API_BASE}/Pedido/RealizarPedido`,{
        method: "POST",
        headers: {'Content-Type': 'application/json', 'Authorization': `Bearer ${token}`},
        body: JSON.stringify(pedido)
    });
}

export async function obtenerPedidosApi(token: string) {
    return await fetch(`${API_BASE}/Pedido/ObtenerPedidos`, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}`
        }
    });
}

export async function obtenerPedidosClienteApi(token: string) {
    return await fetch(`${API_BASE}/Pedido/ObtenerPedidosCliente`, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}`
        }
    });
}

export async function actualizarEstadoPedidoApi(pedido: ActualizarEstadoPedidoModel, token: string){
    return await fetch(`${API_BASE}/Pedido/ActualizarEstadoPedido`,{
        method: "PUT",
        headers: {'Content-Type': 'application/json', 'Authorization': `Bearer ${token}`},
        body: JSON.stringify(pedido)
    });
}

    