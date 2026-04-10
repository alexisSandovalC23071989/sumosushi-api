import type { IFormulario } from "../interfaces/IFormulario";

import { PresentacionModel } from "./PresentacionModel";

export class NuevaPresentacionProductoModel implements IFormulario {
    IdPresentacion: number;
    Precio: number;
    Presentacion: string;
    constructor(model: PresentacionModel){
        this.IdPresentacion = model.IdPresentacion;
        this.Precio = 0;
        this.Presentacion = model.Presentacion;
    }
    validar(): string[] {
        const errores: string[] = [];
        const esCero = (valor: number) : boolean=> valor === 0;

        if (esCero(this.IdPresentacion)) {
            errores.push("El id de la presentación es obligatorio.");
        }
        return errores;
    }  
}