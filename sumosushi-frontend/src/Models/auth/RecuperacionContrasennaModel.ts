import type { IFormulario } from "../interfaces/IFormulario";

export class RecuperacionContrasennaModel implements IFormulario{
    correo = "";
    token = "";
    contrasenna = "";
    confirmarContrasenna = "";

    validar(): string[] {
        const errores: string[] = [];
        const soloEspacios = (valor: string): boolean => valor.trim() === "";
    
        if (soloEspacios(this.correo)) {
            errores.push("El correo no puede estar vacío.");
        } else if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(this.correo)) {
            errores.push("El formato del correo no es válido. Ejemplo: usuario@correo.com");
        }
    
        if (soloEspacios(this.token)) {
            errores.push("El token no puede estar vacío.");
        } else if (this.token.length !== 6) {
            errores.push("El token debe tener exactamente 6 caracteres.");
        }
    
        if (soloEspacios(this.contrasenna)) {
            errores.push("La contraseña no puede estar vacía.");
        } else if (this.contrasenna.length < 6) {
            errores.push("La contraseña debe tener al menos 6 caracteres.");
        }
    
        if (soloEspacios(this.confirmarContrasenna)) {
            errores.push("La confirmación de contraseña no puede estar vacía.");
        } else if (this.confirmarContrasenna.length < 6) {
            errores.push("La confirmación de contraseña debe tener al menos 6 caracteres.");
        } else if (this.confirmarContrasenna !== this.contrasenna) {
            errores.push("Las contraseñas no coinciden.");
        }    
        return errores;
    }    
}
