using System.ComponentModel.DataAnnotations;

namespace SumoSushiApp_BackEnd.DTOs.Direccion
{
    public class DireccionCrearDTO
    {
        [Required(ErrorMessage = "El canton es obligatorio")]
        public int IdCanton { get; set; }
        [Required(ErrorMessage = "El distrito es obligatorio")]
        public short IdDistrito { get; set; }
        [Required(ErrorMessage = "El detalle de la dirección es obligatorio")]
        public string? Detalle { get; set; }
    }
}