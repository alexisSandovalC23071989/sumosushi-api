
export class CantonModel {
    IdCanton: number;
    Canton: string;

    constructor(idCanton: number = 0, canton: string = '') {
        this.IdCanton = idCanton;
        this.Canton = canton;
    }
}