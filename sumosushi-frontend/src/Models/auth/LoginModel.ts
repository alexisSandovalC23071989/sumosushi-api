import type { IFormulario } from "../interfaces/IFormulario";

export class LoginModel implements IFormulario{
    correo: string = "";
    contrasenna: string = "";

    validar(): string[]{
        const errores: string[] = [];
        const soloEspacios = (valor: string) : boolean=> valor.trim() === "";

        if (soloEspacios(this.correo)) {
            errores.push("El correo no puede estar vacío.");
        } else if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(this.correo)) {
            errores.push("El formato del correo no es válido. Ejemplo: usuario@correo.com");
        }

        if (soloEspacios(this.contrasenna)) {
            errores.push("La contraseña no puede estar vacía.");
        } else if (this.contrasenna.length < 6) {
            errores.push("La contraseña debe tener al menos 6 caracteres.");
        }
        return errores;
    }
}