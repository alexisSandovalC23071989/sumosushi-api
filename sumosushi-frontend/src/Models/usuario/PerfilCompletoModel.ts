import { DireccionCompletaModel } from "../direccion/DireccionCompletaModel";

export class PerfilCompletoUsuarioModel {
  Nombre: string;
  Apellidos: string;
  Correo: string;
  Telefonos: string[];
  Direcciones: DireccionCompletaModel[];

  constructor(
    nombre: string = '',
    apellidos: string = '',
    correo: string = '',
    telefonos: string[] = [],
    direcciones: DireccionCompletaModel[] = []
  ) {
    this.Nombre = nombre;
    this.Apellidos = apellidos;
    this.Correo = correo;
    this.Telefonos = telefonos;
    this.Direcciones = direcciones;
  }
}