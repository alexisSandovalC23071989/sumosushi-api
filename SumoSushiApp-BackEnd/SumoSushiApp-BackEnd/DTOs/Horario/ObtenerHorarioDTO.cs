namespace SumoSushiApp_BackEnd.DTOs.Horario
{
    public class ObtenerHorarioDTO
    {
        public int IdDia { get; set; }
        public string? Dia { get; set; }
        public TimeSpan HoraApertura { get; set; }
        public TimeSpan HoraCierre { get; set; }
    }
}
