
export class DistritoModel {
    IdCanton: number;
    IdDistrito: number;
    Distrito: string;
    constructor(idDistrito: number, distrito: string, idCanton: number) {
        this.IdDistrito = idDistrito;
        this.Distrito = distrito;
        this.IdCanton = idCanton;
    }
}
    