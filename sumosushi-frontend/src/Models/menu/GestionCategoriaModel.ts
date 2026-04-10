import type { IFormulario } from "../interfaces/IFormulario";

export class GestionCategoriaModel implements IFormulario {
    IdCategoria: number;
    Descripcion: string;
    Estado: string;
    EstadoBool: boolean;
    constructor(idCategoria: number, descripcion: string, estado: string) {
        this.IdCategoria = idCategoria;
        this.Descripcion = descripcion;
        this.Estado = estado;
        this.EstadoBool = estado === "Activo";
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