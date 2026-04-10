
import { ProductoMenuModel } from "./ProductoMenuModel";

export class CategoriaMenuModel {
    IdCategoria: number;
    Categoria: string;
    Productos: ProductoMenuModel[];
    constructor(
        idCategoria: number,
        categoria: string,
        productos: ProductoMenuModel[]
    ){
        this.IdCategoria = idCategoria;
        this.Categoria = categoria;
        this.Productos = productos;
    }
    static fromJson(json: any): CategoriaMenuModel {
        return new CategoriaMenuModel(
            json.idCategoria,
            json.categoria,
            json.productos.map(ProductoMenuModel.fromJson)
        );
    }
}

