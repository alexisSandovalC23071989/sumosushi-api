
export class PresentacionCompletaModel {
    public IdPresentacionProducto: number;
    public IdProducto: number;
    public DetallePresentacion: string;
    public Precio: number;


    constructor(
        idPresentacionProducto: number,
        idProducto: number,
        detallePresentacion: string,
        precio: number
    ) {
        this.IdPresentacionProducto = idPresentacionProducto;
        this.IdProducto = idProducto;
        this.DetallePresentacion = detallePresentacion;
        this.Precio = precio;
    }

    static fromJson(json: any): PresentacionCompletaModel {
        return new PresentacionCompletaModel(
            json.idPresentacionProducto,
            json.idProducto,
            json.detallePresentacion,
            json.precio
        );
    }
}
