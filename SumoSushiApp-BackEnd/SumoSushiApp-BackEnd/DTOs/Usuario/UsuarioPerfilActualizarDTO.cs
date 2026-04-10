using System.ComponentModel.DataAnnotations;

namespace SumoSushiApp_BackEnd.DTOs.Usuario
{
    public class UsuarioPerfilActualizarDTO
    {
        public long IdUsuario { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "Los apellidos son obligatorios.")]
        public string? Apellidos { get; set; }
    }
}