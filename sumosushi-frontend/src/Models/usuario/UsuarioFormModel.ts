
export class UsuarioFormModel {
    IdUsuario: number;
    Nombre: string;
    Apellidos: string;
    Correo: string;
    IdRol: number;
    Rol: string;

    constructor(
        idUsuario: number,
        nombre: string,
        apellidos: string,
        correo: string,
        idRol: number,
        rol: string,
    ) {
        this.IdUsuario = idUsuario;
        this.Nombre = nombre;
        this.Apellidos = apellidos;
        this.Correo = correo;
        this.IdRol = idRol;
        this.Rol = rol;
    }
}
