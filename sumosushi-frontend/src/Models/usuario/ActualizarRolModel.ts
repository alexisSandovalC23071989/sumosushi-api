export class ActualizarRolModel {
    IdUsuario: number;
    IdRol: number;

    constructor(idUsuario: number, idRol: number) {
        this.IdUsuario = idUsuario;
        this.IdRol = idRol;
    }
}