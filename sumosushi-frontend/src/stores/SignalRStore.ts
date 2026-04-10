import { writable } from 'svelte/store';
import { HUB_PEDIDOS_URL } from '../Config';
import * as signalR from '@microsoft/signalr';
import { obtenerAccesToken } from '../services/AuthService';
import { audioHabilitado } from './AudioStore';
import { cargarPedidos } from './PedidosStore';

export let activeConnection = writable(false);

let connection: signalR.HubConnection | null = null;

// Conexion SignalR
export const iniciarRecepcionPedidos = (onNuevoPedido: (notificacion: any) => void) => {
    if (connection && 
        (connection.state === signalR.HubConnectionState.Connected || 
            connection.state === signalR.HubConnectionState.Connecting)) {
        return;
    }

    connection = new signalR.HubConnectionBuilder()
        .withUrl(HUB_PEDIDOS_URL, {
            accessTokenFactory: async () => {
                const token = await obtenerAccesToken();
                return token ?? "";
            }
        })
        .withAutomaticReconnect()
        .build();

    connection.on("RecibirPedido", async (notificacion) => {

        // *** ACTIVAR REFRESH DE PEDIDOS ***
        const token = await obtenerAccesToken();
        if (!token) return;
        await cargarPedidos(token);
        audioHabilitado.sonar();
        console.log("Nuevo pedido detectado", notificacion);
        if (onNuevoPedido) onNuevoPedido(notificacion);
    });

    connection.start()
        .then(() => {
            console.log("SignalR conectado");
            activeConnection.set(true);
        })
        .catch(console.error);
};
export const detenerRecepcionPedidos = () => {
    if (connection) {
        connection.stop()
            .then(() => {
                console.log("SignalR desconectado");
                activeConnection.set(false);
                connection = null;
            })
            .catch(console.error);
    }
};
