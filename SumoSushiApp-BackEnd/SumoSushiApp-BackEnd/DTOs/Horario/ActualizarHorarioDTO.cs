using System.ComponentModel.DataAnnotations;

namespace SumoSushiApp_BackEnd.DTOs.Horario
{
    public class ActualizarHorarioDTO
    {
        [Required]
        [Range(1, 7, ErrorMessage = "El Id del día debe estar entre 1 y 7.")]
        public int IdDia { get; set; }
        [Required(ErrorMessage = "La hora de apertura es obligatoria.")]
        public TimeSpan HoraApertura { get; set; }
        [Required(ErrorMessage = "La hora de cierre es obligatoria.")]
        public TimeSpan HoraCierre { get; set; }
    }
}
