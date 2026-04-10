import PresentacionApi from "../api/PresentacionApi";

import { responseEstatusOk } from "./utils/ResponseStatus";

import { PresentacionModel } from "../models/presentacion/PresentacionModel";
import type { RespuestaModel } from "../models/RespuestaModel";
import { cargarPresentaciones } from "../stores/PresentacionStore";

export async function obtenerPresentacionesService(token: string): Promise<PresentacionModel[]> {
    try {
        const response = await PresentacionApi.obtenerPresentacionesApi(token);
        if (!await responseEstatusOk(response)) {
            return [];
        }

        const respuesta: RespuestaModel = await response.json();

        if (!respuesta.exito) {
            alert(respuesta.mensaje ?? "Error al obtener las presentaciones.");
            return [];
        }

        const datos = Array.isArray(respuesta.datos)
            ? respuesta.datos.map(PresentacionModel.fromJson)
            : [];

        return datos;
    } catch (error: any) {
        alert("Error de conexión. Verifica tu internet e intenta de nuevo.");
        return [];
    }
}

