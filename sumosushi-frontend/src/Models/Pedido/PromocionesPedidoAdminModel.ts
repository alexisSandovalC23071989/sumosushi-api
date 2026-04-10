export class PromocionesPedidoAdminModel {
    NombrePromocion: string;
    Precio: number;
    Cantidad: number;

    constructor(nombre: string, precio: number, cantidad: number) {
        this.NombrePromocion = nombre;
        this.Precio = precio;
        this.Cantidad = cantidad;
    }

    static fromJSON(json: any): PromocionesPedidoAdminModel {
        return new PromocionesPedidoAdminModel(
            json.nombrePromocion,
            json.precio,
            json.cantidad
        );
    }
}
