import type { IFormulario } from "../interfaces/IFormulario";
export class ActualizarEstadoPedidoModel implements IFormulario {
    IdPedido: number;
    IdEstadoPedido: number;
    constructor (idPedido: number, idEstadoPedido: number) {
        this.IdPedido = idPedido;
        this.IdEstadoPedido = idEstadoPedido;
    }
    validar(): string[] {
        const errores: string[] = [];
        if (this.IdPedido <= 0) errores.push("El id del pedido debe ser mayor a 0");
        if (this.IdEstadoPedido <= 0) errores.push("El id del estado debe ser mayor a 0");
        return errores;
    }
}
