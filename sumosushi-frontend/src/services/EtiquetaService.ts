import {obtenerEtiquetasApi} from "../api/EtiquetaApi";

import { responseEstatusOk } from "./utils/ResponseStatus";

import type { RespuestaModel } from "../models/RespuestaModel";
import { EtiquetaModel } from "../models/menu/EtiquetaModel";

// import {etiquetas, cargarEtiquetas, estaEtiquetasVacio} from "../stores/EtiquetasStore";

export async function obtenerEtiquetasService(token: string): Promise<EtiquetaModel[]> {
    try{
        const response: Response = await obtenerEtiquetasApi(token);
        if (!responseEstatusOk(response)) {
                return [];
        }
    
        const respuesta: RespuestaModel = await response.json();
        if (!respuesta.exito) {
            alert(respuesta.mensaje ?? "Error al obtener las etiquetas.");
            return [];
        }

        const datos = Array.isArray(respuesta.datos)
            ? respuesta.datos.map(EtiquetaModel.fromJson)
            : [];
        return datos;

    }catch(error: any){
        alert("Error de conexión. Verifica tu internet e intenta de nuevo.");
        return [];
    }
}
