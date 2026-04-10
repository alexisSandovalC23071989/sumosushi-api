import type { IFormulario } from "../interfaces/IFormulario";

export class NuevaEtiquetaModel implements IFormulario {
    IdEtiqueta: number;
    constructor(idEtiqueta: number){
        this.IdEtiqueta = idEtiqueta;
    }
    validar(): string[] {
        const errores: string[] = [];
        const esCero = (valor: number) : boolean=> valor === 0;

        if (esCero(this.IdEtiqueta)) {
            errores.push("El id de la etiqueta es obligatorio.");
        }
        return errores;
    }  
}