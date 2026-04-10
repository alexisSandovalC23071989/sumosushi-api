import type { IFormulario } from "../interfaces/IFormulario";
import type { PromocionModel } from "./PromocionModel";

export class ActualizarPromocionModel implements IFormulario {
    IdPromocion: number;
    Descripcion: string;
    FechaInicio: string;
    FechaFinal: string;
    Precio: number;
    ImagenUrl: string;
    ImagenFile?: File;
    CanjeablePuntos: boolean;
    Estado: string;

    constructor(promocionModel: PromocionModel){
        this.IdPromocion = promocionModel.IdPromocion;
        this.Descripcion = promocionModel.Descripcion;
        this.FechaInicio = promocionModel.FechaInicio;
        this.FechaFinal = promocionModel.FechaFinal;
        this.Precio = promocionModel.Precio;
        this.ImagenUrl = promocionModel.ImagenUrl;
        this.CanjeablePuntos = promocionModel.CanjeablePuntos;
        this.Estado = promocionModel.Estado;
    }    
    
    validar(): string[] {
        const errores: string[] = [];
        const soloEspacios = (valor: string) : boolean=> valor.trim() === "";

        if (soloEspacios(this.Descripcion)) {
            errores.push("La descripción no puede estar vacía.");
        }

        if (soloEspacios(this.FechaInicio)) {
            errores.push("La fecha de inicio no puede estar vacía.");
        }

        if (soloEspacios(this.FechaFinal)) {
            errores.push("La fecha de finalización no puede estar vacía.");
        }

        if (this.FechaInicio > this.FechaFinal) {
            errores.push("La fecha de inicio no puede ser mayor a la fecha de finalización.");
        }

        if (soloEspacios(this.ImagenUrl)) {
            errores.push("La imagen no puede estar vacía.");
        }

        if (soloEspacios(this.Estado)) {
            errores.push("El estado no puede estar vacío.");
        }

        return errores;
    }
}
