import { PresentacionMenuModel } from "./PresentacionMenuModel";

export class ProductoMenuModel {
    IdProducto: number;
    Nombre: string;
    Descripcion: string;
    ImagenUrl: string;
    CanjeablePuntos: boolean;
    IdCategoria: number;
    Categoria: string;
    Estado: string;
    Etiquetas: string;
    Presentaciones: PresentacionMenuModel[];
    constructor(
        idProducto: number,
        nombre: string,
        descripcion: string,
        imagenUrl: string,
        canjeablePuntos: boolean,
        idCategoria: number,
        categoria: string,
        estado: string,
        etiquetas: string,
        presentaciones: PresentacionMenuModel[]
    ){
        this.IdProducto = idProducto;
        this.Nombre = nombre;
        this.Descripcion = descripcion;
        this.ImagenUrl = imagenUrl;
        this.CanjeablePuntos = canjeablePuntos;
        this.IdCategoria = idCategoria;
        this.Categoria = categoria;
        this.Estado = estado;
        this.Etiquetas = etiquetas;
        this.Presentaciones = presentaciones;
    }
    static fromJson(json: any): ProductoMenuModel {
        return new ProductoMenuModel(
            json.idProducto,
            json.nombre,
            json.descripcion,
            json.imagenUrl,
            json.canjeablePuntos,
            json.idCategoria,
            json.categoria,
            json.estado,
            json.etiquetas,
            json.presentaciones.map(PresentacionMenuModel.fromJson)
        );
    }
}

