using System.ComponentModel.DataAnnotations;

namespace SumoSushiApp_BackEnd.DTOs.Auth
{
    public class AuthRegistroDTO
    {
        [Required]
        public string? Correo { get; set; }
        [Required]
        public string? Nombre { get; set; }
        [Required]
        public string? Apellidos { get; set; }
        [Required]
        public string? Telefono { get; set; }
        [Required]
        public string? Contrasenna { get; set; }

    }
}
