import { 
    obtenerCategoriasApi, 
    deshabilitarCategoriaApi, 
    habilitarCategoriaApi,
    crearCategoriaApi } 
    from "../api/CategoriaApi";

//Utils
import { responseEstatusOk } from "./utils/ResponseStatus";

//Models
import type { RespuestaModel } from "../models/RespuestaModel";
import { GestionCategoriaModel } from "../models/menu/GestionCategoriaModel";

//Stores
import { cargarCategorias } from "../stores/CategoriaStore";
import type { NuevaCategoriaModel } from "../models/menu/NuevaCategoriaModel";

export async function obtenerCategoriasService(token: string): Promise<GestionCategoriaModel[]> {
    try {
        const response = await obtenerCategoriasApi(token);
        if (!await responseEstatusOk(response)) {
            return [];
        }    
        const respuesta: RespuestaModel = await response.json();
        if (!respuesta.exito) {
            return [];
        }    
        return respuesta.datos.map((item: any) => new GestionCategoriaModel(item.idCategoria, item.descripcion, item.estado));
    } catch (error) {
        alert("Error de conexión");
        return [];
    }
}

export async function actualizarEstadoCategoriaService(token: string, categoria: GestionCategoriaModel): Promise<void> {
    const decision = confirm(`¿Estás seguro de ${categoria.Estado === "Activo" ? "deshabilitar" : "habilitar"} la categoría ${categoria.Descripcion}?`);
    if (!decision) return;
    try {
        let response: Response;
        if (categoria.Estado === "Activo") {
            response = await deshabilitarCategoriaApi(token, categoria);
        } else {
            response = await habilitarCategoriaApi(token, categoria);
        }
        
        if (!await responseEstatusOk(response)) {
            return;
        }    
        const respuesta: RespuestaModel = await response.json();
        if (!respuesta.exito) {
            alert(respuesta.mensaje);
        }
        await cargarCategorias(token);                
    } catch (error) {
        alert("Error de conexión");
    }
}

export async function crearCategoriaService(token: string, categoria: NuevaCategoriaModel): Promise<void> {
    const decision = confirm(`¿Estás seguro de crear la categoría ${categoria.Descripcion}?`);
    if (!decision) return;
    try {
        const response = await crearCategoriaApi(token, categoria);
        if (!await responseEstatusOk(response)) {
            return;
        }    
        const respuesta: RespuestaModel = await response.json();
        if (!respuesta.exito) {
            alert(respuesta.mensaje);
            return;
        }
        await cargarCategorias(token);                
    } catch (error) {
        alert("Error de conexión");
    }
}