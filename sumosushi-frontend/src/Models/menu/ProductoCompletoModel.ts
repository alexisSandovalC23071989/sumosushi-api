import { PresentacionCompletaModel } from "./PresentacionCompletaModel";
import { EtiquetaProductoModel } from "./EtiquetaProductoModel";

export class ProductoCompletoModel {
    public IdProducto: number;
    public Nombre: string;
    public Descripcion: string;
    public ImagenUrl: string;
    public CanjeablePuntos: boolean;
    public IdCategoria: number;
    public Categoria: string;
    public Estado: string;

    public Etiquetas: EtiquetaProductoModel[];
    
    public Presentaciones: PresentacionCompletaModel[];

    constructor(
        idProducto: number,
        nombre: string,
        descripcion: string,
        imagenUrl: string,
        canjeablePuntos: boolean,
        idCategoria: number,
        categoria: string,
        estado: string,
        etiquetas: EtiquetaProductoModel[],
        presentaciones: PresentacionCompletaModel[]
    ) {
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

    static fromJson(json: any): ProductoCompletoModel {
        const presentaciones = (json.presentaciones || []).map((p: any) =>
            PresentacionCompletaModel.fromJson(p)
        );
        const etiquetas = (json.etiquetas || []).map((e: any) =>
            EtiquetaProductoModel.fromJson(e)
        );

        return new ProductoCompletoModel(
            json.idProducto,
            json.nombre,
            json.descripcion,
            json.imagenUrl,
            json.canjeablePuntos,
            json.idCategoria,
            json.categoria,
            json.estado,
            etiquetas,
            presentaciones
        );
    }   
}
