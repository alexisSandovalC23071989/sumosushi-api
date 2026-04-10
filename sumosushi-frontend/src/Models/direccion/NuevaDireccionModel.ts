
export class NuevaDireccionModel {
    Detalle: string;
    IdCanton: number;
    IdDistrito: number;
    constructor(detalle: string = '', idCanton: number = 0, idDistrito: number = 0) {
        this.Detalle = detalle;
        this.IdCanton = idCanton;
        this.IdDistrito = idDistrito;
    }
    validar(): string[] {
        const errores: string[] = [];
        const soloEspacios = (valor: string) : boolean=> valor.trim() === "";

        // Validar detalle
        if (soloEspacios(this.Detalle)) {
            errores.push("El detalle no puede estar vacío.");
        }

        // Validar canton
        if (this.IdCanton === 0) {
            errores.push("El cantón no puede estar vacío.");
        }

        // Validar distrito
        if (this.IdDistrito === 0) {
            errores.push("El distrito no puede estar vacío.");
        }

        return errores;
    }

}
