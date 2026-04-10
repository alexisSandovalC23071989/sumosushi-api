export class ProductosPedidoAdminModel {
    NombreProducto: string;
    Presentacion: string;
    Precio: number;
    Cantidad: number;

    constructor(nombre: string, presentacion: string, precio: number, cantidad: number) {
        this.NombreProducto = nombre;
        this.Presentacion = presentacion;
        this.Precio = precio;
        this.Cantidad = cantidad;
    }

    static fromJSON(json: any): ProductosPedidoAdminModel {
        return new ProductosPedidoAdminModel(
            json.nombreProducto,
            json.presentacion,
            json.precio,
            json.cantidad
        );
    }
}