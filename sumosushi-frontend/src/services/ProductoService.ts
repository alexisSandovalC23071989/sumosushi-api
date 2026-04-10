import ProductoApi from "../api/ProductoApi";

import { responseEstatusOk } from "./utils/ResponseStatus";

import { ProductoCompletoModel } from "../models/menu/ProductoCompletoModel";
import type { ActualizarProductoModel } from "../models/producto/ActualizarProductoModel";
import type { RespuestaModel } from "../models/RespuestaModel";
import { cargarProductos } from "../stores/ProductosStore";
import type { NuevoProductoModel } from "../models/producto/NuevoProductoModel";

export async function obtenerProductosService(token: string): Promise<ProductoCompletoModel[]> {
    try {
        const response = await ProductoApi.obtenerProductosApi(token);
        if (!await responseEstatusOk(response)) {
            return [];
        }

        const respuesta: RespuestaModel = await response.json();

        if (!respuesta.exito) {
            alert(respuesta.mensaje ?? "Error al obtener los productos.");
            return [];
        }

        const datos = Array.isArray(respuesta.datos)
            ? respuesta.datos.map(ProductoCompletoModel.fromJson)
            : [];

        return datos;
    } catch (error: any) {
        alert("Error de conexión. Verifica tu internet e intenta de nuevo.");
        return [];
    }
}
export async function editarProductoService(token: string, producto: ActualizarProductoModel): Promise<boolean> {
    try {
        const response = await ProductoApi.actualizarProductoApi(token, producto);
        if (!await responseEstatusOk(response)) {
            return false;
        }

        const respuesta: RespuestaModel = await response.json();

        if (!respuesta.exito) {
            alert(respuesta.mensaje ?? "Error al editar el producto.");
            return false;
        }

        cargarProductos(token);
        alert(respuesta.mensaje);
        
        return true;
    } catch (error: any) {
        alert("Error de conexión. Verifica tu internet e intenta de nuevo.");
        return false;
    }
}
export async function registrarProductoService(token: string, producto: NuevoProductoModel): Promise<boolean> {
    try {
        const response = await ProductoApi.registrarProductoApi(token, producto);
        if (!await responseEstatusOk(response)) {
            return false;
        }

        const respuesta: RespuestaModel = await response.json();

        if (!respuesta.exito) {
            alert(respuesta.mensaje ?? "Error al registrar el producto.");
            return false;
        }

        cargarProductos(token);
        alert(respuesta.mensaje);        
        return true;
        
    } catch (error: any) {
        alert("Error de conexión. Verifica tu internet e intenta de nuevo.");
        return false;
    }
}
