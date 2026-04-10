import type { IFormulario } from "../interfaces/IFormulario";

export class SolicitudRecuperacionContrasennaModel implements IFormulario{
    correo = "";

    validar(): string[]{
        const errores: string[] = [];
        const soloEspacios = (valor: string) : boolean=> valor.trim() === "";

        if (soloEspacios(this.correo)) {
            errores.push("El correo no puede estar vacío.");
        } else if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/ .test(this.correo)) {
            errores.push("El formato del correo no es válido. Ejemplo: usuario@correo.com");
        }
        return errores;
    }
}
