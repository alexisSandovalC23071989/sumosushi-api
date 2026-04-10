
export interface RespuestaModel<T = any> {
    exito: boolean;
    mensaje?: string;
    datos?: T;
  }
  