import type { IFormulario } from "../interfaces/IFormulario";

export class HorarioFormModel implements IFormulario {
    IdDia: number | null;
    HoraApertura: string;
    HoraCierre: string;
  
    constructor(
      idDia: number | null = null,
      horaApertura: string = '',
      horaCierre: string = ''
    ) {
      this.IdDia = idDia;
      this.HoraApertura = horaApertura;
      this.HoraCierre = horaCierre;
    }
  
    esHorarioValido(): boolean {
      return this.HoraApertura < this.HoraCierre;
    }
  
    validar(): string[] {
      const errores: string[] = [];
      const vacio = (v: string) => v.trim() === '';
  
      if (this.IdDia === null || this.IdDia < 1 || this.IdDia > 7) {
        errores.push("Debe seleccionar un día válido.");
      }
  
      if (vacio(this.HoraApertura)) {
        errores.push("La hora de apertura no puede estar vacía.");
      }
  
      if (vacio(this.HoraCierre)) {
        errores.push("La hora de cierre no puede estar vacía.");
      }
  
      if (!vacio(this.HoraApertura) && !vacio(this.HoraCierre) && !this.esHorarioValido()) {
        errores.push("La hora de apertura debe ser menor que la hora de cierre.");
      }
  
      return errores;
    }
  
    toDTO(): HorarioFormModel {
        const formato = (h: string) => h.length === 5 ? h + ":00" : h;
        return new HorarioFormModel(
          this.IdDia,
          formato(this.HoraApertura),
          formato(this.HoraCierre)
        );
      }
  }
  