namespace SumoSushiApp_BackEnd.DTOs.Usuario
{
    public class InfoUsuarioDTO
    {
        public long IdUsuario { get; set; }
        public string? Nombre { get; set; }
        public string? Apellidos { get; set; }
        public string? Correo { get; set; }
        public int IdRol { get; set; }
        public string? Rol { get; set; }

    }
}
