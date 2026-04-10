
import { agregarProducto, obtenerCarrito } from "../api/CarritoApi";

import { responseEstatusOk } from "./utils/ResponseStatus";
import type { RespuestaModel } from "../models/RespuestaModel";

import { CarritoModel } from "../models/carrito/CarritoModel";

export async function obtenerCarritoService(token: string): Promise<CarritoModel[]> {
    try{
        const response = await obtenerCarrito(token);
        if (!await responseEstatusOk(response)) {
            return [];
        }    
        const respuesta: RespuestaModel = await response.json();
        if (!respuesta.exito) {
            return [];
        }    
        return respuesta.datos.map((item: any) => CarritoModel.fromJson(item));
    } catch (error: any) {
        alert("Error de conexión: " + error.message);
        return [];
    }
}

export async function agregarProductoService(token: string, carrito: CarritoModel): Promise<boolean> {
    try {
        const response = await agregarProducto(token, carrito);
        if (!await responseEstatusOk(response)) {
            return false;
        }
        const respuesta: RespuestaModel = await response.json();
        if (!respuesta.exito) {
            alert(respuesta.mensaje);
            return false;
        }
        return true;
    } catch (error: any) {
        alert("Error de conexión: " + error.message);
        return false;
    }
}
