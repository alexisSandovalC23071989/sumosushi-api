import promoApi from "../api/PromocionApi";

import { responseEstatusOk } from "./utils/ResponseStatus";

import type { RespuestaModel } from "../models/RespuestaModel";
import { PromocionModel } from "../models/promocion/PromocionModel";
import { ActualizarPromocionModel } from "../models/promocion/ActualizarPromocionModel";
import { NuevaPromocionModel } from "../models/promocion/NuevaPromocionModel";

import {cargarPromociones} from "../stores/PromocionesStore";

export async function obtenerPromocionesService(token: string): Promise<PromocionModel[]> {
    try {
        const response = await promoApi.obtenerPromocionesApi(token);
        if (!await responseEstatusOk(response)) {
            return [];
        }

        const respuesta: RespuestaModel = await response.json();

        if (!respuesta.exito) {
            alert(respuesta.mensaje ?? "Error al obtener las promociones.");
            return [];
        }

        const datos = Array.isArray(respuesta.datos)
            ? respuesta.datos.map(PromocionModel.fromJson)
            : [];

        return datos;
    } catch (error: any) {
        alert("Error de conexión. Verifica tu internet e intenta de nuevo.");
        return [];
    }
}

export async function editarPromocionService(token: string, promocion: ActualizarPromocionModel): Promise<boolean> {
    try {
        const response = await promoApi.actualizarPromocionApi(token, promocion);
        if (!await responseEstatusOk(response)) {
            return false;
        }

        const respuesta: RespuestaModel = await response.json();

        if (!respuesta.exito) {
            alert(respuesta.mensaje ?? "Error al editar la promoción.");
            return false;
        }

        
        cargarPromociones(token);
        alert(respuesta.mensaje);
        
        return true;
    } catch (error: any) {
        alert("Error de conexión. Verifica tu internet e intenta de nuevo.");
        return false;
    }
}
 export async function registrarPromocionService(token: string, promocion: NuevaPromocionModel): Promise<boolean> {
    try {
        const response = await promoApi.registrarPromocionApi(token, promocion);
        if (!await responseEstatusOk(response)) {
            return false;
        }

        const respuesta: RespuestaModel = await response.json();

        if (!respuesta.exito) {
            alert(respuesta.mensaje ?? "Error al registrar la promoción.");
            return false;
        }

        cargarPromociones(token);
        alert(respuesta.mensaje);
        
        return true;
    } catch (error: any) {
        alert("Error de conexión. Verifica tu internet e intenta de nuevo.");
        return false;
    }
}

