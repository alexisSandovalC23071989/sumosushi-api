import type { IFormulario } from "../interfaces/IFormulario";

export class InfoRestauranteModel implements IFormulario {    
    Nombre?: string;
    Direccion?: string;
    Telefono?: string;
    Correo?: string;
    LogoUrl?: string;
    
    constructor(
        nombre: string = '',
        direccion: string = '',
        telefono: string = '',
        correo: string = '',
        logoUrl: string = ''
    ) {
        this.Nombre = nombre;
        this.Direccion = direccion;
        this.Telefono = telefono;
        this.Correo = correo;
        this.LogoUrl = logoUrl;
    }

    validar(): string[] {
        const errores: string[] = [];
        const soloEspacios = (valor: string | undefined): boolean => !valor || valor.trim() === "";

        // Validar nombre
        if (soloEspacios(this.Nombre)) {
            errores.push("El nombre del restaurante no puede estar vacío.");
        }

        // Validar dirección
        if (soloEspacios(this.Direccion)) {
            errores.push("La dirección del restaurante no puede estar vacía.");
        }

        // Validar teléfono
        if (soloEspacios(this.Telefono)) {
            errores.push("El teléfono del restaurante no puede estar vacío.");
        } else if (this.Telefono!.length < 8 || this.Telefono!.length > 20) {
            errores.push("El teléfono debe tener entre 8 y 20 caracteres.");
        }

        // Validar correo
        if (soloEspacios(this.Correo)) {
            errores.push("El correo del restaurante no puede estar vacío.");
        } else if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(this.Correo!)) {
            errores.push("El formato del correo no es válido. Ejemplo: restaurante@correo.com");
        }

        // Validar URL del logo (opcional, solo si se indica)
        if (this.LogoUrl && this.LogoUrl.trim() !== "") {
            try {
                const url = new URL(this.LogoUrl);
                if (!["http:", "https:"].includes(url.protocol)) {
                    errores.push("La URL del logo debe comenzar con http o https.");
                }
            } catch {
                errores.push("La URL del logo no es válida.");
            }
        }
        return errores;
    }
}