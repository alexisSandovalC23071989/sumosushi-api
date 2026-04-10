
export class HorarioModel {
  IdDia: number;
  Dia: string;
  HoraApertura: string;
  HoraCierre: string;

  constructor(
    idDia: number | null = null,
    dia: string = '',
    horaApertura: string = '',
    horaCierre: string = ''
  ) {
    this.IdDia = idDia!;
    this.Dia = dia;
    this.HoraApertura = horaApertura;
    this.HoraCierre = horaCierre;
  }
}
