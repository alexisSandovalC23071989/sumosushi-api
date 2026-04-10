
export class CarritoModel {
    IdProductoCarritoUsuario: number;
    IdPresentacionProducto: number;
    IdPromocion: number;
    Cantidad: number;
    constructor(
        idProductoCarritoUsuario: number,
        idPresentacionProducto: number,
        idPromocion: number,
        cantidad: number
    ) {
        this.IdProductoCarritoUsuario = idProductoCarritoUsuario;
        this.IdPresentacionProducto = idPresentacionProducto;
        this.IdPromocion = idPromocion;
        this.Cantidad = cantidad;
    }

    static fromJson(json: any): CarritoModel {
        return new CarritoModel(
            json.idProductoCarritoUsuario,
            json.idPresentacionProducto,
            json.idPromocion,
            json.cantidad
        );
    }
    
}

