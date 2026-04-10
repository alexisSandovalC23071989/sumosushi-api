using System.ComponentModel.DataAnnotations;

namespace SumoSushiApp_BackEnd.DTOs.Usuario
{
    public class ObtenerUsuarioPorCorreoDTO
    {
        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
        public string? Correo { get; set; }
    }
}
