
export class PromocionMenuModel {
    IdPromocion: number;
    Nombre: string;
    Descripcion: string;
    FechaInicio: string;
    FechaFinal: string;
    Precio: number;
    ImagenUrl: string;
    CanjeablePuntos: boolean;
    Estado: string;
    constructor(
        idPromocion: number,
        nombre: string,
        descripcion: string,
        fechaInicio: string,
        fechaFinal: string,
        imagenUrl: string,
        precio: number,
        canjeablePuntos: boolean,
        estado: string){
        this.IdPromocion = idPromocion;
        this.Nombre = nombre;
        this.Descripcion = descripcion;
        this.FechaInicio = fechaInicio.substring(0, 10);
        this.FechaFinal = fechaFinal.substring(0, 10);
        this.Precio = precio;
        this.ImagenUrl = imagenUrl;
        this.CanjeablePuntos = canjeablePuntos;
        this.Estado = estado;
    }

    static fromJson(json: any): PromocionMenuModel {
        return new PromocionMenuModel(
            json.idPromocion,
            json.nombre,
            json.descripcion,
            json.fechaInicio,
            json.fechaFinal,
            json.imagenUrl,
            json.precio,
            json.canjeablePuntos,
            json.estado
        );
    }
}