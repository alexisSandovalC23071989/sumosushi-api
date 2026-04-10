
export class DireccionCompletaModel {
  IdDireccion: number;
  Detalle: string;
  IdCanton: number;
  Canton: string;
  IdDistrito: number;
  Distrito: string;  

  constructor(idDireccion: number = 0, canton: string = '', distrito: string = '', detalle: string = '', idCanton: number = 0, idDistrito: number = 0) {
    this.IdDireccion = idDireccion;
    this.Canton = canton;
    this.Distrito = distrito;
    this.Detalle = detalle;
    this.IdCanton = idCanton;
    this.IdDistrito = idDistrito;
  }
}