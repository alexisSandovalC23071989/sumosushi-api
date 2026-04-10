import type { IFormulario } from "../interfaces/IFormulario";

import type { NuevaEtiquetaModel } from "../etiqueta/NuevaEtiquetaModel";
import type { NuevaPresentacionProductoModel } from "../presentacion/NuevaPresentacionProductoModel";

export class NuevoProductoModel implements IFormulario {
    Nombre: string;
    Descripcion: string;
    ImagenFile?: File;
    CanjeablePuntos: boolean;
    IdCategoria: number;

    Etiquetas: NuevaEtiquetaModel[];
    Presentaciones: NuevaPresentacionProductoModel[];
    constructor(){
        this.Nombre = "";
        this.Descripcion = "";
        this.ImagenFile = undefined;
        this.CanjeablePuntos = false;
        this.IdCategoria = 0;
        this.Etiquetas = [];
        this.Presentaciones = [];
    }

    validar(): string[] {
        const errores: string[] = [];
        const soloEspacios = (valor: string) : boolean=> valor.trim() === "";

        if (soloEspacios(this.Nombre)) {
            errores.push("El nombre del producto es obligatorio.");
        }

        if (soloEspacios(this.Descripcion)) {
            errores.push("La descripción es obligatoria.");
        }
        if(this.ImagenFile === undefined) {
            errores.push("La imagen es obligatoria.");
        }
        if(this.IdCategoria == 0) {
            errores.push("La categoría es obligatoria.");
        }
        if(this.Presentaciones.length == 0) {
            errores.push("Al menos una presentación es obligatoria.");
        }
        this.Etiquetas.forEach((etiqueta) => {
            errores.push(...etiqueta.validar());
        });
        this.Presentaciones.forEach((presentacion) => {
            errores.push(...presentacion.validar());
        });
        return errores;
    }      

    toFormData(): FormData {
        const formData = new FormData();
    
        // Se excluye la imagen y se incluye todo lo demás
        const modelSinImagen = {
            Nombre: this.Nombre,
            Descripcion: this.Descripcion,
            CanjeablePuntos: this.CanjeablePuntos,
            IdCategoria: this.IdCategoria,
            Etiquetas: this.Etiquetas,
            Presentaciones: this.Presentaciones
        };
    
        formData.append("modelJson", JSON.stringify(modelSinImagen));
        formData.append("ImagenFile", this.ImagenFile!);
    
        return formData;
    }
}


