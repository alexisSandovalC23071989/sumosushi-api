// APIs
import { obtenerCantonesApi, obtenerDireccionesUsuarioApi, obtenerDistritosApi } from "../api/DireccionApi";

// Utils
import { responseEstatusOk } from "./utils/ResponseStatus";

// Models
import { CantonModel } from "../models/direccion/CantonModel";
import { DistritoModel } from "../models/direccion/DistritoModel";
import type { RespuestaModel } from "../models/RespuestaModel";
import { DireccionUsuarioModel } from "../models/direccion/DireccionUsuarioModel";

export async function obtenerCantonesService(token: string): Promise<CantonModel[]> {
    try {
        const response = await obtenerCantonesApi(token);
        if (!await responseEstatusOk(response)) {
            return [];
        }

        const respuesta: RespuestaModel = await response.json();
        if (!respuesta.exito) {
            alert(respuesta.mensaje);
            return [];
        }
        if(!respuesta.datos){
            alert("Error al obtener cantones");
            return [];
        }
    
        return respuesta.datos.map((item: any) => new CantonModel(item.idCanton, item.canton));
    } catch (error: any) {
        alert("Error de conexión. Por favor verifica tu conexión e intenta de nuevo.");    
        return [];    
    }
}

export async function obtenerDistritosService(token: string): Promise<DistritoModel[]> {
    try {
        const response = await obtenerDistritosApi(token);
        if (!await responseEstatusOk(response)) {
            return [];
        }
    
        const respuesta: RespuestaModel = await response.json();
        if (!respuesta.exito) {
            alert(respuesta.mensaje);
            return [];
        }
        if(!respuesta.datos){
            alert("Error al obtener distritos");
            return [];
        }
    
        return respuesta.datos.map((item: any) => new DistritoModel(item.idDistrito, item.distrito, item.idCanton));

    } catch (error: any) {
        alert("Error de conexión. Por favor verifica tu conexión e intenta de nuevo.");    
        return [];    
    }
}

export async function obtenerDireccionesUsuarioService(token: string): Promise<DireccionUsuarioModel[]> {
    try {
        const response = await obtenerDireccionesUsuarioApi(token);
        if (!await responseEstatusOk(response)) {
            return [];
        }
    
        const respuesta: RespuestaModel = await response.json();
        if (!respuesta.exito) {
            alert(respuesta.mensaje);
            return [];
        }
        if(!respuesta.datos){
            alert("Error al obtener direcciones");
            return [];
        }
    
        return respuesta.datos.map((item: any) => DireccionUsuarioModel.fromJson(item));
    } catch (error: any) {
        alert("Error de conexión. Por favor verifica tu conexión e intenta de nuevo.");    
        return [];    
    }
}
