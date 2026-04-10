import type { IFormulario } from "../interfaces/IFormulario";

export class NuevaCategoriaModel implements IFormulario {
    Descripcion: string;
    constructor(descripcion: string) {
        this.Descripcion = descripcion;
    }
    validar(): string[] {
        const errores: string[] = [];
        const soloEspacios = (valor: string) : boolean=> valor.trim() === "";

        if (soloEspacios(this.Descripcion)) {
            errores.push("La descripción no puede estar vacía.");
        } 
        return errores;
    }
}
