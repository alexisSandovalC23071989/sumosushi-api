using System.ComponentModel.DataAnnotations;

namespace SumoSushiApp_BackEnd.DTOs.Auth
{
    public class AuthContrasennasUsuarioDTO
    {
        public string? Contrasenna { get; set; } = string.Empty;
        
        public string? NuevaContrasenna { get; set; } = string.Empty;
        
        public long IdUsuario { get; set; }

        public AuthContrasennasUsuarioDTO(SolicitudCambiarContrasennaDTO model, long idUsuario)
        {
            Contrasenna = model.ContrasennaActual;
            NuevaContrasenna = model.NuevaContrasenna;
            IdUsuario = idUsuario;
        }
    }
}
