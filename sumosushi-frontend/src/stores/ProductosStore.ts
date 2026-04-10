import {writable, get} from 'svelte/store';

import {ProductoCompletoModel} from '../models/menu/ProductoCompletoModel';

import {obtenerProductosService} from '../services/ProductoService';

export let productos = writable<ProductoCompletoModel[]>([]);

export async function cargarProductos(token: string): Promise<void> {
    productos.set(await obtenerProductosService(token));
}
export function estaProductosVacio(): boolean {
    return get(productos).length === 0;
}

export function getProductosPorEstado(estado?:string): Array<ProductoCompletoModel> {
    if (!estado) return get(productos);
    return get(productos).filter(producto => producto.Estado === estado);
}



