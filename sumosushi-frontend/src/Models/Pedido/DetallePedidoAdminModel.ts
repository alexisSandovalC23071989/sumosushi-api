
export class DetallePedidoAdminModel {
    IdPresentacionProducto: number;
    IdPromocion: number;
    Nombre: string;
    Cantidad: number;
    Precio: number;
    NotaCliente: string;
    constructor (
        nombre: string,
        cantidad: number,
        precio: number,
        notaCliente?: string,
        idPresentacionProducto?: number,
        idPromocion?: number,
    ) {
        this.IdPresentacionProducto = idPresentacionProducto || 0;
        this.IdPromocion = idPromocion || 0;
        this.Nombre = nombre;
        this.Cantidad = cantidad;
        this.Precio = precio;
        this.NotaCliente = notaCliente || "";
    }

    static fromJSON(json: any): DetallePedidoAdminModel {
        return new DetallePedidoAdminModel(
            json.Nombre,
            json.Cantidad,
            json.Precio,
            json.NotaCliente,
            json.IdPresentacionProducto,
            json.IdPromocion
        );
    }
}
