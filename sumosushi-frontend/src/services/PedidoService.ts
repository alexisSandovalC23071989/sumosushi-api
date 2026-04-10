import type { RespuestaModel } from "../models/RespuestaModel";
import { NuevoPedidoModel } from "../models/Pedido/NuevoPedidoModel";
import { registrarPedidoApi } from "../api/PedidoApi";
import { responseEstatusOk } from "./utils/ResponseStatus";
import { PedidoAdminModel } from "../models/Pedido/PedidoAdminModel";
import { obtenerPedidosApi, obtenerPedidosClienteApi } from "../api/PedidoApi";
import { actualizarEstadoPedidoApi } from "../api/PedidoApi";
import { ActualizarEstadoPedidoModel } from "../models/Pedido/ActualizarEstadoPedidoModel";

export async function nuevoPedidoService(pedido: NuevoPedidoModel, token: string): Promise<boolean> {
    try {
        const response: Response = await registrarPedidoApi(pedido, token);
        if (!await responseEstatusOk(response)) return false;

        const json: RespuestaModel = await response.json();
        if (!json.exito) {
            alert(json.mensaje);
            return false;
        }        
        alert(json.mensaje);
        return true;
    } catch (error: any) {
        alert("Error de conexión. Verifica tu internet e intenta de nuevo.");
        return false;
    }
}

export async function obtenerPedidosService(token: string): Promise<PedidoAdminModel[]> {
    try {
        const response: Response = await obtenerPedidosApi(token);
        if (!await responseEstatusOk(response)) return [];

        const json: RespuestaModel = await response.json();
        if (!json.exito) {
            alert(json.mensaje);
            return [];
        }
        if (!Array.isArray(json.datos)) {
            console.error("Los datos no son un arreglo:", json.datos);
            return [];
        }
        
        return (json.datos as any[])
            .map(PedidoAdminModel.fromJSON)
            .sort((a, b) => a.IdEstadoPedido - b.IdEstadoPedido);
    } catch (error: any) {
        alert("Error de conexión. Verifica tu internet e intenta de nuevo.");
        console.error("Error al obtener pedidos:", error);
        return [];
    }
}

export async function obtenerMisPedidosService(token: string): Promise<PedidoAdminModel[]> {
    try {
        const response: Response = await obtenerPedidosClienteApi(token);
        if (!await responseEstatusOk(response)) return [];

        const json: RespuestaModel = await response.json();
        if (!json.exito) {
            alert(json.mensaje);
            return [];
        }
        if (!Array.isArray(json.datos)) {
            console.error("Los datos no son un arreglo:", json.datos);
            return [];
        }
        
        return (json.datos as any[])
            .map(PedidoAdminModel.fromJSON)
            .sort((a, b) => a.IdEstadoPedido - b.IdEstadoPedido);
    } catch (error: any) {
        alert("Error de conexión. Verifica tu internet e intenta de nuevo.");
        console.error("Error al obtener pedidos:", error);
        return [];
    }
}

export async function actualizarEstadoPedidoService(pedido: ActualizarEstadoPedidoModel, token: string): Promise<boolean> {
    try {
        const errores = pedido.validar();
        if (errores.length > 0) {
            alert(errores.join("\n"));
            return false;
        }
        const response: Response = await actualizarEstadoPedidoApi(pedido, token);
        if (!await responseEstatusOk(response)) return false;

        const json: RespuestaModel = await response.json();
        if (!json.exito) {
            alert(json.mensaje);
            return false;
        }        
        alert(json.mensaje);
        return true;
    } catch (error: any) {
        alert("Error de conexión. Verifica tu internet e intenta de nuevo.");
        return false;
    }
}
