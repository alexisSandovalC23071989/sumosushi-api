
import { responseEstatusOk } from "./utils/ResponseStatus";
import { InfoRestauranteModel } from "../models/restaurante/InfoRestauranteModel";
import { actualizarInfoRestauranteApi, obtenerInfoRestauranteApi } from "../api/RestauranteApi";
import { infoSesion } from "../stores/AuthStores";
import {get} from 'svelte/store';
import { goToGestionInfoRest } from "../routes/Routes";


export async function obtenerInfoRestauranteService(): Promise<InfoRestauranteModel> {
    try {
        const response = await obtenerInfoRestauranteApi();
        if (!await responseEstatusOk(response)) {
            return new InfoRestauranteModel();            
        }

        const json = await response.json();

        if (!json.datos) {
            return new InfoRestauranteModel();            
        }

        const datos = json.datos;

        return new InfoRestauranteModel(
            datos.nombre,
            datos.direccion,
            datos.telefono,
            datos.correo,
            datos.logoUrl
        );
    } catch (error: any) {
        alert("Error de conexión. Por favor verifica tu conexión e intenta de nuevo.");    
        return new InfoRestauranteModel();    
    }
}

export async function actualizarInfoRestauranteService(infoRestaurante: InfoRestauranteModel): Promise<void> {
    try {
        const errores = infoRestaurante.validar();
        if (errores.length > 0){
            alert(errores.join("\n"));
            return;
        }
        
        const response = await actualizarInfoRestauranteApi(infoRestaurante, get(infoSesion).Jwt);
        if (!await responseEstatusOk(response)) {
            return;
        }

        const json = await response.json();

        if (!json.exito) {
            alert(json.mensaje ?? "Error al actualizar la información del restaurante.");
            return;
        }     

        alert("Información del restaurante actualizada exitosamente.");
        window.location.reload();   

    } catch (error: any) {
        alert("Error de conexión. Por favor verifica tu conexión e intenta de nuevo.");
    }
}
