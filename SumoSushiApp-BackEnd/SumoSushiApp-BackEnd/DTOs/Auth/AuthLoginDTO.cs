using System.ComponentModel.DataAnnotations;

namespace SumoSushiApp_BackEnd.DTOs.Usuario
{
    public class AuthLoginDTO
    {
        [Required]        
        public string? Correo { get; set; }
        [Required]
        public string? Contrasenna { get; set; }
    }
}
