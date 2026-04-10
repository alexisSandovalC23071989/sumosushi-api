using System.ComponentModel.DataAnnotations;

namespace SumoSushiApp_BackEnd.DTOs.Usuario
{
    public class ActualizarRolDTO
    {
        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "El Id de usuario debe ser un número positivo.")]
        public long IdUsuario { get; set; }

        [Required]
        [Range(1, 3, ErrorMessage = "El Id de rol debe ser un número entre 1 y 3.")]
        public int IdRol { get; set; }
    }
}
