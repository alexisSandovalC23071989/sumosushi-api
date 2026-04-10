import type { EtiquetaModel } from "./EtiquetaModel";

export class EtiquetaProductoModel {
    public IdProducto: number;
    public IdEtiqueta: number;
    public DetalleEtiqueta: string;
    constructor(idProducto: number = 0, idEtiqueta: number, detalleEtiqueta: string) {
        this.IdProducto = idProducto;
        this.IdEtiqueta = idEtiqueta;
        this.DetalleEtiqueta = detalleEtiqueta;
    }

    static fromJson(json: any): EtiquetaProductoModel {
        return new EtiquetaProductoModel(
            json.idProducto,
            json.idEtiqueta,
            json.detalleEtiqueta
        );
    }

    static fromEtiquetaModel(etiquetaModel: EtiquetaModel): EtiquetaProductoModel {
        return new EtiquetaProductoModel(
            0,
            etiquetaModel.IdEtiqueta,
            etiquetaModel.Etiqueta
        );
    }
}

