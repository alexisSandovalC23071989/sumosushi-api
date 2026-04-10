import { obtenerVersionApi, obtenerMenuApi } from "../api/MenuApi";
import { responseEstatusOk } from "./utils/ResponseStatus";
import type { RespuestaModel } from "../models/RespuestaModel";
import { MenuCompletoModel } from "../models/menu/MenuCompletoModel";

export async function obtenerVersionService(): Promise<number> {
    try{
        const response = await obtenerVersionApi();
        if (!await responseEstatusOk(response)) {
            return 0;
        }
        const respuesta: RespuestaModel = await response.json();
        if (!respuesta.exito) {
            alert(respuesta.mensaje ?? "Error al obtener la version del menu.");
            return 0;
        }
        return respuesta.datos;
    } catch (error: any) {
        alert("Error de conexión. Verifica tu internet e intenta de nuevo.");
        return 0;
    }
}

export async function obtenerMenuCompletoService(): Promise<MenuCompletoModel> {
    try{
        const response = await obtenerMenuApi();
        if (!await responseEstatusOk(response)) {
            return new MenuCompletoModel(0, [], []);
        }
        const respuesta: RespuestaModel = await response.json();
        if (!respuesta.exito) {
            alert(respuesta.mensaje ?? "Error al obtener el menu completo.");
            return new MenuCompletoModel(0, [], []);
        }
        const menu = MenuCompletoModel.fromJson(respuesta.datos);
        console.log(menu);
        return menu;
    } catch (error: any) {
        alert("Error de conexión. Verifica tu internet e intenta de nuevo.");
        return new MenuCompletoModel(0, [], []);
    }
}    