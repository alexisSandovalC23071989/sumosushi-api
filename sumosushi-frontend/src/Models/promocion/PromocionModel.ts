
export class PromocionModel {
    IdPromocion: number;
    Nombre: string;
    Descripcion: string;
    FechaInicio: string;
    FechaFinal: string;
    Precio: number;
    ImagenUrl: string;
    ImagenFile?: File;
    CanjeablePuntos: boolean;
    Estado: string;
    EstadoBool: boolean;

    constructor(
        idPromocion: number,
        nombre: string,
        descripcion: string,
        fechaInicio: string,
        fechaFinal: string,
        imagenUrl: string,
        precio: number,
        canjeablePuntos: boolean,
        estado: string) {
            this.IdPromocion = idPromocion;
            this.Nombre = nombre;
            this.Descripcion = descripcion;
            this.FechaInicio = fechaInicio.substring(0, 10);
            this.FechaFinal = fechaFinal.substring(0, 10);
            this.ImagenUrl = imagenUrl;
            this.Precio = precio;
            this.CanjeablePuntos = canjeablePuntos;
            this.Estado = estado;
            this.EstadoBool = estado === "Activo";
    }

    static fromJson(json: any): PromocionModel {
        return new PromocionModel(
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
