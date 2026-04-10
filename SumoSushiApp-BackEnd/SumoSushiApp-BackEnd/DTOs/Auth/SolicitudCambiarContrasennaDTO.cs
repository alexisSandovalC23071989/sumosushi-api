using System.ComponentModel.DataAnnotations;

namespace SumoSushiApp_BackEnd.DTOs.Auth
{
    public class SolicitudCambiarContrasennaDTO
    {
        [Required(ErrorMessage = "La contraseña actual es obligatoria.")]
        [StringLength(255, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.", MinimumLength = 6)]
        public string ContrasennaActual { get; set; } = string.Empty;

        [Required(ErrorMessage = "La nueva contraseña es obligatoria.")]
        [StringLength(255, ErrorMessage = "La nueva contraseña debe tener al menos 6 caracteres.", MinimumLength = 6)]
        public string NuevaContrasenna { get; set; } = string.Empty;

    }
}
