import type { IFormulario } from "../interfaces/IFormulario";

export class RegistroUsuarioModel implements IFormulario{
    nombre = "";
    apellidos = "";
    telefono = "";
    correo = "";
    contrasenna = "";
    confirmarContrasenna = "";

    validar(): string[]{
        const errores: string[] = [];
        const soloEspacios = (valor: string) : boolean=> valor.trim() === "";

        // Validar nombre
        if (soloEspacios(this.nombre)) {
            errores.push("El nombre no puede estar vacío.");
        }

        // Validar apellidos
        if (soloEspacios(this.apellidos)) {
            errores.push("Los apellidos no pueden estar vacíos.");
        }

        // Validar teléfono
        if (soloEspacios(this.telefono)) {
            errores.push("El teléfono no puede estar vacío.");
        } else if (!/^\d{8,15}$/.test(this.telefono)) {
            errores.push("El teléfono debe contener al menos 8 numeros");
        }

        // Validar correo
        if (soloEspacios(this.correo)) {
            errores.push("El correo no puede estar vacío.");
        } else if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(this.correo)) {
            errores.push("El formato del correo no es válido. Ejemplo: usuario@correo.com");
        }

        // Validar contraseña
        if (soloEspacios(this.contrasenna)) {
            errores.push("La contraseña no puede estar vacía.");
        } else if (this.contrasenna.length < 6) {
            errores.push("La contraseña debe tener al menos 6 caracteres.");
        }

        // Validar confirmación de contraseña
        if (this.contrasenna !== this.confirmarContrasenna) {
            errores.push("Las contraseñas no coinciden.");
        }

        return errores;
    }
}