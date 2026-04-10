import type { IFormulario } from "../interfaces/IFormulario";
import type { ProductoCompletoModel } from "../menu/ProductoCompletoModel";
import type { EtiquetaProductoModel } from "../menu/EtiquetaProductoModel";
import { PresentacionCompletaModel } from "../menu/PresentacionCompletaModel";
import { presentaciones } from "../../stores/PresentacionStore";
import { get } from "svelte/store";

export class ActualizarProductoModel implements IFormulario {
    IdProducto: number;
    Nombre: string;
    Descripcion: string;
    ImagenUrl: string;
    ImagenFile?: File;
    CanjeablePuntos: boolean;
    IdCategoria: number;
    Estado: string;
    
    Etiquetas: EtiquetaProductoModel[];
    Presentaciones: PresentacionCompletaModel[];

    constructor(productoModel: ProductoCompletoModel) {
        this.IdProducto = productoModel.IdProducto;
        this.Nombre = productoModel.Nombre;
        this.Descripcion = productoModel.Descripcion;
        this.ImagenUrl = productoModel.ImagenUrl;
        this.CanjeablePuntos = productoModel.CanjeablePuntos;
        this.IdCategoria = productoModel.IdCategoria;
        this.Estado = productoModel.Estado;
        this.Etiquetas = productoModel.Etiquetas;
    
        const todasLasPresentaciones = get(presentaciones);
    
        this.Presentaciones = todasLasPresentaciones.map(pres => {
            const existente = productoModel.Presentaciones.find(
                p => p.DetallePresentacion.trim().toLowerCase() === pres.Presentacion.trim().toLowerCase()
            );
    
            return existente ?? new PresentacionCompletaModel(
                0,
                productoModel.IdProducto,
                pres.Presentacion,
                0
            );
        });
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
        if(this.IdCategoria == 0) {
            errores.push("La categoría es obligatoria.");
        }
        if(soloEspacios(this.Estado)) {
            errores.push("El estado es obligatorio.");
        }
        return errores;
    }  

    toFormData(): FormData{
        const formData = new FormData();

        const modelSinImagen = {
            IdProducto: this.IdProducto,
            Nombre: this.Nombre,
            Descripcion: this.Descripcion,
            ImagenUrl: this.ImagenUrl,
            CanjeablePuntos: this.CanjeablePuntos,
            IdCategoria: this.IdCategoria,
            Estado: this.Estado,
            Etiquetas: this.Etiquetas,
            Presentaciones: this.Presentaciones
        };

        formData.append("modelJson", JSON.stringify(modelSinImagen));
        formData.append("ImagenFile", this.ImagenFile!);
        
        return formData;
    }
}