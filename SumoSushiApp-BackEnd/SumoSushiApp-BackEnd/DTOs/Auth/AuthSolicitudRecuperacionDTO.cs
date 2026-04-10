using System.ComponentModel.DataAnnotations;

namespace SumoSushiApp_BackEnd.DTOs.Auth
{
    public class AuthSolicitudRecuperacionDTO
    {
        [Required]
        public string? Correo { get; set; }
    }
}
