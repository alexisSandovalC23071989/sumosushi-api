using System.ComponentModel.DataAnnotations;

namespace SumoSushiApp_BackEnd.DTOs.Auth
{
    public class AuthRecuperacionContrasennaDTO
    {
        [Required]
        [EmailAddress]
        [StringLength(255)]
        public string? Correo { get; set; }
        [Required]
        [StringLength(255, MinimumLength = 6)]
        public string? Token { get; set; }
        [Required]
        [StringLength(255, MinimumLength = 6)]
        public string? Contrasenna { get; set; }
        }
}
