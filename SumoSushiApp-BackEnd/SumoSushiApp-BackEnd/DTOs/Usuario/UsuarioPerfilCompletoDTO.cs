using SumoSushiApp_BackEnd.DTOs.Direccion;

namespace SumoSushiApp_BackEnd.DTOs.Usuario
{
    public class UsuarioPerfilCompletoDTO
    {        
        public string? Nombre { get; set; }
        public string? Apellidos { get; set; }
        public string? Correo { get; set; }
        public long PuntosAcumulados { get; set; }
        public IEnumerable<string> Telefonos { get; set; } = [];
        public IEnumerable<DireccionCompletaDTO> Direcciones { get; set; } = [];
    }
}
