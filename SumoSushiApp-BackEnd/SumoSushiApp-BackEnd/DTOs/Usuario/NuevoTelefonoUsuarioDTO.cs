using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SumoSushiApp_BackEnd.DTOs.Usuario
{
    public class NuevoTelefonoUsuarioDTO
    {
        public long IdUsuario { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "El número de teléfono debe tener entre 8 y 20 caracteres.")]
        public string? Telefono { get; set; }
    }
}
