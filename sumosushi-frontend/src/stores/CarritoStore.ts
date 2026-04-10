import { writable, get, derived } from "svelte/store";

import { CarritoModel } from "../models/carrito/CarritoModel";
import { obtenerCarritoService } from "../services/CarritoService";

export let carrito = writable<CarritoModel[]>([]);

export async function cargarCarrito(token: string) {   
    const carritoDesdeAPI = await obtenerCarritoService(token);
    carrito.set(carritoDesdeAPI);
    console.log(get(carrito));
}

export function estaCarritoVacio(): boolean {
    return get(carrito).length === 0;
}

// export async function cantidadCarrito(): Promise<number> {
//     const cantidadCarrito = get(carrito).reduce((total, item) => total + item.Cantidad, 0);
//     return cantidadCarrito;
// }

export const cantidadItems = derived(carrito, ($carrito) =>
    $carrito.reduce((total, item) => total + item.Cantidad, 0)
);

export default {
    cargarCarrito,
    estaCarritoVacio,
    cantidadItems
}

