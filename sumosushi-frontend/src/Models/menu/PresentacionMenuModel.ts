
export class PresentacionMenuModel {
    IdPresentacionProducto: number;
    IdProducto: number;
    DetallePresentacion: string;
    Precio: number;
    constructor(
        idPresentacionProducto: number,
        idProducto: number,
        detallePresentacion: string,
        precio: number
    ){
        this.IdPresentacionProducto = idPresentacionProducto;
        this.IdProducto = idProducto;
        this.DetallePresentacion = detallePresentacion;
        this.Precio = precio;
    }
    static fromJson(json: any): PresentacionMenuModel {
        return new PresentacionMenuModel(
            json.idPresentacionProducto,
            json.idProducto,
            json.detallePresentacion,
            json.precio
        );
    }
}

