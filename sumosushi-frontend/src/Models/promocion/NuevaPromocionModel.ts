import type { IFormulario } from "../interfaces/IFormulario";

export class NuevaPromocionModel implements IFormulario {
    Nombre: string;
    Descripcion: string;
    FechaInicio: string;
    FechaFinal: string;
    Precio: number;
    CanjeablePuntos: boolean;
    ImagenFile?: File;
    constructor(){
        this.Nombre = "";
        this.Descripcion = "";
        this.FechaInicio = "";
        this.FechaFinal = "";
        this.Precio = 0;
        this.CanjeablePuntos = false;
        this.ImagenFile = undefined;
    }
    validar(): string[] {
        const errores: string[] = [];
        if (this.Nombre.trim() === "") errores.push("El nombre es obligatorio");
        if (this.Descripcion.trim() === "") errores.push("La descripción es obligatoria");
        if (this.FechaInicio.trim() === "") errores.push("La fecha de inicio es obligatoria");
        if (this.FechaFinal.trim() === "") errores.push("La fecha de finalización es obligatoria");
        if (this.Precio <= 0) errores.push("El precio debe ser mayor a 0");
        if (this.ImagenFile === undefined) errores.push("La imagen es obligatoria");
        return errores;
    }
}
