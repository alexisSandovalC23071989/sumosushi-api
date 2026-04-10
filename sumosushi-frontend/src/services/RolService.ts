import { RolModel } from "../models/rol/RolModel";
import { obtenerRolesApi } from "../api/RolApi";
import { responseEstatusOk } from "./utils/ResponseStatus";
import type { RespuestaModel } from "../models/RespuestaModel";

export async function obtenerRolesService(token: string): Promise<RolModel[]> {
    try {
        const response: Response = await obtenerRolesApi(token);
        if (!await responseEstatusOk(response)) return [];

        const json: RespuestaModel = await response.json();
        return json.datos.map((item: any) => new RolModel(item.idRol, item.rol));
    } catch (error: any) {
        alert("Error de conexión. Verifica tu internet e intenta de nuevo.");
        return [];
    }
}