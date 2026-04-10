
export class DireccionUsuarioModel {
    IdDireccion: number;
    Detalle: string;
    IdCanton: number;
    Canton: string;
    IdDistrito: number;
    Distrito: string;
    constructor(idDireccion: number = 0, detalle: string = '', idCanton: number = 0, canton: string = '', idDistrito: number = 0, distrito: string = '') {
        this.IdDireccion = idDireccion;
        this.Detalle = detalle;
        this.IdCanton = idCanton;
        this.Canton = canton;
        this.IdDistrito = idDistrito;
        this.Distrito = distrito;
    }

    static fromJson(json: any): DireccionUsuarioModel {
        return new DireccionUsuarioModel(json.idDireccion, json.detalle, json.idCanton, json.canton, json.idDistrito, json.distrito);
    }
}
